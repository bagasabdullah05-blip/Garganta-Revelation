using System.Collections.Generic;
using UnityEngine;
using Garganta.Core;
using Garganta.Data;
using Garganta.Grid;
using Garganta.Units;
using Garganta.UI;

namespace Garganta.Combat
{
    // Damage formula per COMBAT_SYSTEMS.md + M2 skills/status/items. Pure statics covered by EditMode tests.
    public class CombatManager : MonoBehaviour
    {
        public static int PhysicalBase(int atk, float weaponMult, float classMult, int def)
            => Mathf.Max(1, Mathf.RoundToInt(atk * weaponMult * classMult - def * 0.5f));

        public static int MagicalBase(int mag, float spellMult, int mdef)
            => Mathf.Max(1, Mathf.RoundToInt(mag * spellMult - mdef * 0.5f));

        public static float ElevationMult(int attElev, int defElev)
        {
            if (attElev > defElev) return 1.1f;
            if (attElev < defElev) return 0.9f;
            return 1.0f;
        }

        public static int Finalize(int baseDmg, float tri, float elev, bool crit, float variance)
        {
            float f = baseDmg * tri * elev * (crit ? 1.5f : 1f) * variance;
            return Mathf.Max(1, Mathf.RoundToInt(f));
        }

        public static float HitChance(int acc, int eva, float triBonus, float elevBonus)
            => Mathf.Clamp(acc - eva + triBonus + elevBonus, 20f, 99f);

        public static int HealAmount(int mag, float power, int healPctBonus)
            => Mathf.Max(1, Mathf.RoundToInt(mag * power * (1f + healPctBonus / 100f)));

        public static List<Unit> UnitsInRadius(Vector2Int center, int radius, List<Unit> list)
        {
            var result = new List<Unit>();
            foreach (var u in list)
                if (u.IsAlive && GridManager.HexDistance(center, u.Coord) <= radius) result.Add(u);
            return result;
        }

        public void Attack(Unit attacker, Unit defender)
        {
            if (Strike(attacker, defender, false, 1f, 0f, false) > 0)
                attacker.AddMastery(attacker.ClassId, 2);
        }

        // Returns damage dealt (0 = miss). Shared by basic attacks and damage skills.
        public int Strike(Unit attacker, Unit defender, bool magical, float power, float ignoreDefPct, bool alwaysHit)
        {
            var grid = FindAnyObjectByType<GridManager>();
            int attElev = grid != null ? grid.Tiles[attacker.Coord.x, attacker.Coord.y].Elevation : 0;
            int defElev = grid != null ? grid.Tiles[defender.Coord.x, defender.Coord.y].Elevation : 0;

            int tileDef = grid != null ? Balance.DefBonus(grid.Tiles[defender.Coord.x, defender.Coord.y].Type) : 0;
            float tri = WeaponTriangle.GetMultiplier(attacker.Stats.Weapon, defender.Stats.Weapon);
            float elev = ElevationMult(attElev, defElev);
            float elevAcc = attElev > defElev ? 10f : attElev < defElev ? -10f : 0f;

            int rawDef = (magical ? defender.Stats.MDEF : defender.Stats.DEF) + defender.BuffDef + tileDef;
            int effDef = Mathf.Max(0, Mathf.RoundToInt(rawDef * (1f - ignoreDefPct)));
            int effEva = defender.Stats.Eva + defender.BuffEva;
            int baseDmg = magical
                ? MagicalBase(attacker.Stats.MAG + attacker.BuffMag, attacker.Stats.WeaponMult * power, effDef)
                : PhysicalBase(attacker.Stats.ATK + attacker.BuffAtk, attacker.Stats.WeaponMult * power, attacker.Stats.ClassMult, effDef);

            float variance = Random.Range(0.9f, 1.1f);
            bool crit = Random.value < 0.1f; // M2 flat 10% crit (Dagger first-strike in M4)
            float hit = HitChance(attacker.Stats.Acc, effEva, WeaponTriangle.TriangleAccBonus(attacker.Stats.Weapon, defender.Stats.Weapon), elevAcc);

            if (!alwaysHit && Random.Range(0f, 100f) > hit)
            {
                Popup(defender.transform.position, "MISS", Color.gray);
                EventBus.Log($"{attacker.UnitName} missed {defender.UnitName}");
                return 0;
            }

            int dmg = Finalize(baseDmg, tri, elev, crit, variance);
            defender.TakeDamage(dmg);
            EventBus.Damage(attacker.UnitName, dmg, defender.UnitName);
            EventBus.Log($"{attacker.UnitName} hit {defender.UnitName} for {dmg}{(crit ? " CRIT" : "")}");

            Popup(defender.transform.position, dmg.ToString(), crit ? Color.red : Color.white);
            var shake = FindAnyObjectByType<CameraShake>();
            if (shake != null) shake.AddShake(crit ? 0.5f : 0.2f);
            return dmg;
        }

        public void ResolveSkill(Unit caster, Skill skill, Unit primary, List<Unit> allies, List<Unit> foes)
        {
            if (primary == null && !skill.SelfOnly) return;
            if (!caster.UseMP(skill.CostMP))
            {
                EventBus.Log($"{caster.UnitName}: not enough MP for {skill.Name}");
                return;
            }
            caster.AddMastery(caster.ClassId, 2);
            EventBus.Log($"{caster.UnitName} uses {skill.Name}!");

            List<Unit> targets;
            if (skill.SelfOnly) targets = new List<Unit> { caster };
            else if (skill.TargetsAllies)
                targets = skill.AoE > 0 ? UnitsInRadius(caster.Coord, skill.AoE, allies) : new List<Unit> { primary };
            else
                targets = skill.AoE > 0 ? UnitsInRadius(primary.Coord, skill.AoE, foes) : new List<Unit> { primary };

            foreach (var t in targets)
            {
                if (t == null) continue;
                if (!t.IsAlive && skill.Effect != SkillEffect.Heal) continue;
                switch (skill.Effect)
                {
                    case SkillEffect.Damage:
                        int dmg = Strike(caster, t, skill.Magical, skill.Power, skill.IgnoreDefPct, skill.AlwaysHit);
                        if (skill.StunTurns > 0 && t.IsAlive && dmg > 0)
                        {
                            t.StunTurns = skill.StunTurns;
                            EventBus.Log($"{t.UnitName} is stunned!");
                        }
                        if (skill.StealGold && dmg > 0)
                        {
                            int g = 15 + caster.Level * 5;
                            Inventory.Gold += g;
                            EventBus.Log($"{caster.UnitName} stole {g}G!");
                        }
                        break;
                    case SkillEffect.Heal:
                        int amt = skill.HealIsPct
                            ? Mathf.Max(1, Mathf.RoundToInt(t.Stats.MaxHP * skill.Power / 100f))
                            : HealAmount(caster.Stats.MAG + caster.BuffMag, skill.Power, StaffBonus(caster));
                        t.Heal(amt);
                        if (skill.Cleanse) t.StunTurns = 0;
                        Popup(t.transform.position, "+" + amt, Color.green);
                        EventBus.Log($"{caster.UnitName} heals {t.UnitName} for {amt}");
                        break;
                    case SkillEffect.BuffAtk: t.BuffAtk += (int)skill.Power; t.BuffTurns = skill.Duration; BuffLog(t, "ATK"); break;
                    case SkillEffect.BuffDef: t.BuffDef += (int)skill.Power; t.BuffTurns = skill.Duration; BuffLog(t, "DEF"); break;
                    case SkillEffect.BuffEva: t.BuffEva += (int)skill.Power; t.BuffTurns = skill.Duration; BuffLog(t, "EVA"); break;
                    case SkillEffect.BuffMag: t.BuffMag += (int)skill.Power; t.BuffTurns = skill.Duration; BuffLog(t, "MAG"); break;
                }
            }
        }

        void BuffLog(Unit t, string stat)
        {
            Popup(t.transform.position, stat + " UP", Color.cyan);
            EventBus.Log($"{t.UnitName}'s {stat} rose!");
        }

        static int StaffBonus(Unit c) => c.Equipped.TryGetValue(EquipSlot.Weapon, out var e) ? e.HealPct : 0;

        public void UseItem(Unit user, Consumable item, Unit allyTarget, List<Unit> allies, List<Unit> foes)
        {
            if (!Inventory.Take(item.Id))
            {
                EventBus.Log($"No {item.Name} left!");
                return;
            }
            if (item.BombAll)
            {
                foreach (var f in foes)
                    if (f.IsAlive)
                    {
                        f.TakeDamage(100);
                        Popup(f.transform.position, "100", Color.yellow);
                    }
                EventBus.Log($"{user.UnitName} threw a Bomb!");
                return;
            }
            if (item.Revive)
            {
                foreach (var a in allies)
                    if (!a.IsAlive)
                    {
                        a.HP = 1;
                        var sr = a.GetComponent<SpriteRenderer>();
                        if (sr != null) sr.color = Color.white;
                        Popup(a.transform.position, "REVIVE", Color.green);
                        EventBus.Log($"{a.UnitName} revived!");
                        return;
                    }
                Inventory.Add(item.Id); // refund: nobody down
                EventBus.Log("No one to revive.");
                return;
            }
            Unit t = allyTarget != null && allyTarget.IsAlive ? allyTarget : user;
            if (item.HealHP > 0) t.Heal(item.HealHP);
            if (item.HealMP > 0) t.MP = Mathf.Min(t.Stats.MaxMP, t.MP + item.HealMP);
            if (item.Cleanse) t.StunTurns = 0;
            Popup(t.transform.position, "+" + item.HealHP, Color.green);
            EventBus.Log($"{user.UnitName} used {item.Name} on {t.UnitName}");
        }

        void Popup(Vector3 pos, string text, Color color)
        {
            var cui = FindAnyObjectByType<CombatUI>();
            if (cui != null) cui.SpawnText(pos, text, color);
        }
    }
}
