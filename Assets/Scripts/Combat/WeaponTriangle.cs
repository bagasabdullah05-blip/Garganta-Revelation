using Garganta.Core;

namespace Garganta.Combat
{
    // Sword > Axe > Spear > Sword (1.1x / 0.9x), Bow <> Staff. Dagger neutral.
    public static class WeaponTriangle
    {
        public static float GetMultiplier(WeaponType atk, WeaponType def)
        {
            if (Beats(atk, def)) return 1.1f;
            if (Beats(def, atk)) return 0.9f;
            return 1.0f;
        }

        static bool Beats(WeaponType a, WeaponType b)
        {
            if (a == WeaponType.Sword && b == WeaponType.Axe) return true;
            if (a == WeaponType.Axe && b == WeaponType.Spear) return true;
            if (a == WeaponType.Spear && b == WeaponType.Sword) return true;
            if (a == WeaponType.Bow && b == WeaponType.Staff) return true;
            if (a == WeaponType.Staff && b == WeaponType.Bow) return true;
            return false;
        }

        public static float TriangleAccBonus(WeaponType atk, WeaponType def)
            => GetMultiplier(atk, def) > 1f ? 10f : GetMultiplier(atk, def) < 1f ? -10f : 0f;
    }
}
