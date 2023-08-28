using System.Reflection.Metadata;

namespace TextRPG_Team
{
    public enum ItemType
    {
        Equipment,
        Consumable,
    }

    public class Equipment : Item
    {
        public Equipment(string name, string description, int atk, int def) : base(name, description, ItemType.Equipment, atk, def)
        {
        }
    }

    public class Consumable : Item
    {
        public int Count { get; private set; }
        public Consumable(string name, string description, int atk, int def) : base(name, description, ItemType.Consumable, atk, def)
        {
        }
    }

    public class Item
    {
        public string Name { get; }
        public string Description { get; }

        public ItemType Type { get; }

        public int Atk { get; }
        public int Def { get; }

        public bool IsEquiped { get; set; }

        public Item(string name, string description, ItemType type, int atk, int def)
        {
            Name = name;
            Description = description;
            Atk = atk;
            Def = def;
            Type = type;

            IsEquiped = false;
        }

    }
}
