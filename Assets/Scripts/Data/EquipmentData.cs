using System.Collections.Generic;
using Garganta.Core;

namespace Garganta.Data
{
    public enum EquipSlot { Weapon, Armor, Helmet, Accessory }
    public enum Rarity { Common, Uncommon, Rare, Epic, Legendary }

    public struct Equipment
    {
        public string Id;
        public string Name;
        public EquipSlot Slot;
        public WeaponType Weapon;
        public int ATK, DEF, MAG, MDEF, SPD, Move, Range, Acc, Eva;
        public int HealPct; // staves: healing bonus %
        public Rarity Rarity;
        public int Price;
    }

    public struct Consumable
    {
        public string Id;
        public string Name;
        public int HealHP;
        public int HealMP;
        public bool Revive;
        public bool Cleanse;
        public bool BombAll; // fixed damage to all enemies
        public int Price;
        public string Desc;
    }

    // Full lists per EQUIPMENT_SYSTEM.md (base numbers; rarity shown in UI).
    public static class EquipmentData
    {
        static Equipment W(string id, string name, WeaponType w, int atk, int price, Rarity r = Rarity.Common, int range = 0, int mag = 0, int heal = 0)
            => new Equipment { Id = id, Name = name, Slot = EquipSlot.Weapon, Weapon = w, ATK = atk, MAG = mag, HealPct = heal, Range = range, Price = price, Rarity = r };
        static Equipment A(string id, string name, EquipSlot slot, int def, int mdef, int price, Rarity r = Rarity.Common, int atk = 0, int mag = 0, int spd = 0, int move = 0)
            => new Equipment { Id = id, Name = name, Slot = slot, Weapon = WeaponType.None, DEF = def, MDEF = mdef, ATK = atk, MAG = mag, SPD = spd, Move = move, Price = price, Rarity = r };

        public static readonly List<Equipment> Weapons = new List<Equipment>
        {
            W("rusty_sword", "Rusty Sword", WeaponType.Sword, 5, 0),
            W("iron_sword", "Iron Sword", WeaponType.Sword, 10, 150),
            W("steel_sword", "Steel Sword", WeaponType.Sword, 15, 400, Rarity.Uncommon),
            W("mythril_sword", "Mythril Sword", WeaponType.Sword, 22, 900, Rarity.Rare),
            W("flametongue", "Flametongue", WeaponType.Sword, 28, 1500, Rarity.Epic),
            W("blight_edge", "Blight Edge", WeaponType.Sword, 30, 1800, Rarity.Epic),
            W("shadowfang", "Shadowfang", WeaponType.Sword, 35, 2500, Rarity.Legendary),
            W("excalibur", "Excalibur", WeaponType.Sword, 40, 5000, Rarity.Legendary),
            W("wooden_spear", "Wooden Spear", WeaponType.Spear, 6, 0),
            W("iron_lance", "Iron Lance", WeaponType.Spear, 11, 150),
            W("partisan", "Partisan", WeaponType.Spear, 17, 450, Rarity.Uncommon),
            W("dragoon_lance", "Dragoon Lance", WeaponType.Spear, 24, 1000, Rarity.Rare),
            W("wyrmkiller", "Wyrmkiller", WeaponType.Spear, 32, 2000, Rarity.Epic),
            W("gungnir", "Gungnir", WeaponType.Spear, 38, 4500, Rarity.Legendary),
            W("hand_axe", "Hand Axe", WeaponType.Axe, 8, 0),
            W("battle_axe", "Battle Axe", WeaponType.Axe, 13, 180),
            W("great_axe", "Great Axe", WeaponType.Axe, 19, 500, Rarity.Uncommon),
            W("titan_axe", "Titan Axe", WeaponType.Axe, 27, 1200, Rarity.Rare),
            W("executioner", "Executioner", WeaponType.Axe, 35, 2600, Rarity.Legendary),
            W("short_bow", "Short Bow", WeaponType.Bow, 7, 0, Rarity.Common, -1),
            W("longbow", "Longbow", WeaponType.Bow, 12, 160),
            W("hunter_bow", "Hunter Bow", WeaponType.Bow, 18, 450, Rarity.Uncommon),
            W("ice_bow", "Ice Bow", WeaponType.Bow, 25, 1100, Rarity.Rare),
            W("phantom_bow", "Phantom Bow", WeaponType.Bow, 33, 2200, Rarity.Epic, 1),
            W("artemis", "Artemis", WeaponType.Bow, 39, 4800, Rarity.Legendary, 1),
            W("grimoire", "Grimoire", WeaponType.Tome, 0, 0, Rarity.Common, 0, 6),
            W("fire_tome", "Fire Tome", WeaponType.Tome, 0, 160, Rarity.Common, 0, 11),
            W("blizzard_tome", "Blizzard Tome", WeaponType.Tome, 0, 420, Rarity.Uncommon, 0, 16),
            W("thunder_tome", "Thunder Tome", WeaponType.Tome, 0, 1000, Rarity.Rare, 0, 23),
            W("void_tome", "Void Tome", WeaponType.Tome, 0, 1900, Rarity.Epic, 0, 31),
            W("bahamut", "Bahamut", WeaponType.Tome, 0, 4600, Rarity.Legendary, 0, 38),
            W("wooden_staff", "Wooden Staff", WeaponType.Staff, 0, 0, Rarity.Common, 0, 3, 10),
            W("iron_staff", "Iron Staff", WeaponType.Staff, 0, 150, Rarity.Common, 0, 6, 15),
            W("materia_staff", "Materia Staff", WeaponType.Staff, 0, 500, Rarity.Uncommon, 0, 10, 20),
            W("light_staff", "Light Staff", WeaponType.Staff, 0, 1100, Rarity.Rare, 0, 15, 30),
            W("seraph_staff", "Seraph Staff", WeaponType.Staff, 0, 3000, Rarity.Legendary, 0, 22, 50),
        };

        public static readonly List<Equipment> Armors = new List<Equipment>
        {
            A("cloth", "Cloth Garb", EquipSlot.Armor, 3, 1, 0),
            A("leather_armor", "Leather Armor", EquipSlot.Armor, 6, 2, 120),
            A("chainmail", "Chainmail", EquipSlot.Armor, 10, 3, 350, Rarity.Uncommon),
            A("plate", "Plate Armor", EquipSlot.Armor, 16, 4, 800, Rarity.Rare, 0, 0, -1),
            A("mythril_armor", "Mythril Armor", EquipSlot.Armor, 22, 6, 1500, Rarity.Epic),
            A("dragon_mail", "Dragon Mail", EquipSlot.Armor, 28, 10, 3000, Rarity.Legendary),
            A("mage_robe", "Mage Robe", EquipSlot.Armor, 2, 5, 200, Rarity.Common, 0, 3),
            A("mystic_robe", "Mystic Robe", EquipSlot.Armor, 4, 10, 700, Rarity.Rare, 0, 5),
            A("archmage_robe", "Archmage Robe", EquipSlot.Armor, 6, 16, 1800, Rarity.Epic, 0, 8),
        };

        public static readonly List<Equipment> Helmets = new List<Equipment>
        {
            A("leather_helm", "Leather Helm", EquipSlot.Helmet, 2, 1, 80),
            A("iron_helm", "Iron Helm", EquipSlot.Helmet, 4, 2, 200),
            A("wizard_hat", "Wizard Hat", EquipSlot.Helmet, 1, 4, 300, Rarity.Uncommon, 0, 3),
            A("bronze_helm", "Bronze Helm", EquipSlot.Helmet, 6, 3, 500, Rarity.Rare),
            A("crystal_circlet", "Crystal Circlet", EquipSlot.Helmet, 3, 8, 1200, Rarity.Epic, 0, 4),
            A("dragon_helm", "Dragon Helm", EquipSlot.Helmet, 10, 5, 2500, Rarity.Legendary, 4),
        };

        public static readonly List<Equipment> Accessories = new List<Equipment>
        {
            A("speed_ring", "Speed Ring", EquipSlot.Accessory, 0, 0, 250, Rarity.Common, 0, 0, 3),
            A("power_band", "Power Band", EquipSlot.Accessory, 0, 0, 250, Rarity.Common, 3),
            A("magic_orb", "Magic Orb", EquipSlot.Accessory, 0, 0, 250, Rarity.Common, 0, 3),
            A("iron_charm", "Iron Charm", EquipSlot.Accessory, 5, 0, 400, Rarity.Uncommon),
            A("agility_boots", "Agility Boots", EquipSlot.Accessory, 0, 0, 800, Rarity.Rare, 0, 0, 5, 1),
            A("berserker_belt", "Berserker Belt", EquipSlot.Accessory, -5, 0, 900, Rarity.Rare, 10),
            A("guardian_ring", "Guardian Ring", EquipSlot.Accessory, 8, 8, 1500, Rarity.Epic),
            A("vampire_fang", "Vampire Fang", EquipSlot.Accessory, 0, 0, 1800, Rarity.Epic, 4),
            A("aether_stone", "Aether Stone", EquipSlot.Accessory, 3, 3, 2200, Rarity.Epic, 3, 3, 3),
            A("phoenix_pinion", "Phoenix Pinion", EquipSlot.Accessory, 2, 2, 3000, Rarity.Legendary),
            A("royal_signet", "Royal Signet", EquipSlot.Accessory, 4, 4, 4000, Rarity.Legendary, 4, 4, 4),
        };

        public static readonly List<Consumable> Consumables = new List<Consumable>
        {
            new Consumable { Id = "potion", Name = "Potion", HealHP = 50, Price = 50, Desc = "Restore 50 HP" },
            new Consumable { Id = "hi_potion", Name = "Hi-Potion", HealHP = 150, Price = 200, Desc = "Restore 150 HP" },
            new Consumable { Id = "elixir", Name = "Elixir", HealHP = 50, HealMP = 20, Price = 300, Desc = "Restore 50 HP + 20 MP" },
            new Consumable { Id = "ether", Name = "Ether", HealMP = 30, Price = 100, Desc = "Restore 30 MP" },
            new Consumable { Id = "antidote", Name = "Antidote", Cleanse = true, Price = 30, Desc = "Cleanse stun" },
            new Consumable { Id = "phoenix_down", Name = "Phoenix Down", Revive = true, Price = 500, Desc = "Revive ally at 1 HP" },
            new Consumable { Id = "tent", Name = "Tent", HealHP = 9999, HealMP = 9999, Price = 1000, Desc = "Full restore" },
            new Consumable { Id = "bomb", Name = "Bomb", BombAll = true, Price = 200, Desc = "100 dmg to ALL enemies" },
        };

        public static Equipment FindWeapon(string id) => Find(Weapons, id);
        public static Equipment FindArmor(string id) => Find(Armors, id);
        public static Equipment FindHelmet(string id) => Find(Helmets, id);
        public static Equipment FindAccessory(string id) => Find(Accessories, id);
        public static Consumable FindConsumable(string id) => Consumables.Find(c => c.Id == id);

        // Search every gear list (base UI + roster restore).
        public static Equipment FindAny(string id)
        {
            if (string.IsNullOrEmpty(id)) return default;
            foreach (var list in new[] { Weapons, Armors, Helmets, Accessories })
            {
                int i = list.FindIndex(e => e.Id == id);
                if (i >= 0) return list[i];
            }
            return default;
        }

        public static string SlotName(EquipSlot s)
        {
            switch (s)
            {
                case EquipSlot.Weapon: return "Weapon";
                case EquipSlot.Armor: return "Armor";
                case EquipSlot.Helmet: return "Helmet";
                default: return "Accessory";
            }
        }

        static Equipment Find(List<Equipment> list, string id)
        {
            int i = list.FindIndex(e => e.Id == id);
            return i >= 0 ? list[i] : list[0];
        }

        public static string RarityColor(Rarity r)
        {
            switch (r)
            {
                case Rarity.Uncommon: return "green";
                case Rarity.Rare: return "cyan";
                case Rarity.Epic: return "magenta";
                case Rarity.Legendary: return "yellow";
                default: return "white";
            }
        }
    }
}
