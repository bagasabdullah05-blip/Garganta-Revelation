using UnityEngine;
using Garganta.AI;
using Garganta.Core;

namespace Garganta.Units
{
    public class Unit : MonoBehaviour
    {
        public string UnitName = "Unit";
        public bool IsPlayer;
        public AIBehavior Behavior = AIBehavior.Aggressive;
        public UnitStats Stats = new UnitStats();
        public Vector2Int Coord;
        public float CTB;
        public int HP;
        public bool IsAlive => HP > 0;

        public void Init(string id, string display, bool player, UnitStats s, Vector2Int c)
        {
            name = id;
            UnitName = display;
            IsPlayer = player;
            Stats = s;
            Coord = c;
            HP = s.MaxHP;
            CTB = 0f;
        }

        public void GainCTB(float amount)
        {
            if (IsAlive) CTB += amount;
        }

        public void ResetCTB() => CTB = 0f;

        public void TakeDamage(int amount)
        {
            HP = Mathf.Max(0, HP - amount);
            if (!IsAlive)
            {
                var sr = GetComponent<SpriteRenderer>();
                if (sr != null) sr.color = Color.gray;
            }
        }

        public void Heal(int amount) => HP = Mathf.Min(Stats.MaxHP, HP + amount);
    }
}
