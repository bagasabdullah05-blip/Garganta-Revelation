using UnityEngine;
using Garganta.Core;
using Garganta.Units;
using Garganta.UI;

namespace Garganta.Combat
{
    // Damage formula per COMBAT_SYSTEMS.md. Pure statics are covered by EditMode tests.
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

        public void Attack(Unit attacker, Unit defender, bool magical = false, float power = 1f)
        {
            var grid = FindAnyObjectByType<Grid.GridManager>();
            int attElev = grid != null ? grid.Tiles[attacker.Coord.x, attacker.Coord.y].Elevation : 0;
            int defElev = grid != null ? grid.Tiles[defender.Coord.x, defender.Coord.y].Elevation : 0;

            int tileDef = grid != null ? Balance.DefBonus(grid.Tiles[defender.Coord.x, defender.Coord.y].Type) : 0;
            float tri = WeaponTriangle.GetMultiplier(attacker.Stats.Weapon, defender.Stats.Weapon);
            float elev = ElevationMult(attElev, defElev);
            float elevAcc = attElev > defElev ? 10f : attElev < defElev ? -10f : 0f;

            int effDef = Mathf.Max(0, (magical ? defender.Stats.MDEF : defender.Stats.DEF) + tileDef);
            int baseDmg = magical
                ? MagicalBase(attacker.Stats.MAG, attacker.Stats.WeaponMult * power, effDef)
                : PhysicalBase(attacker.Stats.ATK, attacker.Stats.WeaponMult * power, attacker.Stats.ClassMult, effDef);

            float variance = Random.Range(0.9f, 1.1f);
            bool crit = Random.value < 0.1f; // M1 flat 10% crit
            float hit = HitChance(attacker.Stats.Acc, defender.Stats.Eva, WeaponTriangle.TriangleAccBonus(attacker.Stats.Weapon, defender.Stats.Weapon), elevAcc);

            if (Random.Range(0f, 100f) > hit)
            {
                var cui = FindAnyObjectByType<CombatUI>();
                if (cui != null) cui.SpawnText(defender.transform.position, "MISS", Color.gray);
                EventBus.Log($"{attacker.UnitName} missed {defender.UnitName}");
                return;
            }

            int dmg = Finalize(baseDmg, tri, elev, crit, variance);
            defender.TakeDamage(dmg);
            EventBus.Damage(attacker.UnitName, dmg, defender.UnitName);
            EventBus.Log($"{attacker.UnitName} hit {defender.UnitName} for {dmg}{(crit ? " CRIT" : "")}");

            var cui2 = FindAnyObjectByType<CombatUI>();
            if (cui2 != null) cui2.SpawnText(defender.transform.position, dmg.ToString(), crit ? Color.red : Color.white);
            var shake = FindAnyObjectByType<CameraShake>();
            if (shake != null) shake.AddShake(crit ? 0.5f : 0.2f);
        }
    }
}
