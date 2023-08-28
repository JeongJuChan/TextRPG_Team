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
        public int Atk { get; }
        public int Def { get; }

        public bool IsEquiped { get; set; }

        public Equipment(string name, string description, int atk, int def) : base(name, description, ItemType.Equipment)
        {
            Atk = atk;
            Def = def;
            IsEquiped = false;
        }
    }

    public class Consumable : Item
    {
        public int Count { get; set; }
        public int Amount { get; }
        Action<int> consumeAction;

        public void Consume()
        {
            consumeAction?.Invoke(Amount);
        }

        public Consumable(string name, string description, Action<int> action, int amount ,int count) : base(name, description, ItemType.Consumable)
        {
            consumeAction = action;
            Amount = amount;
            Count = count;
        }
    }

    public class Item
    {
        public string Name { get; }
        public string Description { get; }

        public ItemType Type { get; }

        

        public Item(string name, string description, ItemType type)
        {
            Name = name;
            Description = description;
            
            Type = type;
        }

    }
}
