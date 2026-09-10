using Garganta.Core;

namespace Garganta.Units
{
    [System.Serializable]
    public class UnitStats
    {
        public int MaxHP = 100;
        public int ATK = 12;
        public int DEF = 10;
        public int MAG = 3;
        public int MDEF = 5;
        public int SPD = 5;
        public int Move = 5;
        public int Range = 1;
        public int Acc = 90;
        public int Eva = 10;
        public WeaponType Weapon = WeaponType.Sword;
        public float WeaponMult = 1f;
        public float ClassMult = 1f;

        public UnitStats Clone() => (UnitStats)MemberwiseClone();
    }
}
