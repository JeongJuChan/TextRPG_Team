
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


        public Equipment(string name, string description, int atk, int def, bool isHave, bool isEquiped, int buyGold, int sellGold) : base(name, description, ItemType.Equipment, isHave, buyGold, sellGold)
        {
            Atk = atk;
            Def = def;
            IsEquiped = isEquiped;
        }
    }

    public class Consumable : Item
    {
        public int Amount { get; }
        Action<int> consumeAction;

        public Consumable(string name, string description, Action<int> action, int amount ,int count, bool isHave, int buyGold, int sellGold) : base(name, description, ItemType.Consumable, isHave, buyGold, sellGold)
        {
            consumeAction = action;
            Amount = amount;
            Count = count;
        }

        public void Consume()
        {
            consumeAction?.Invoke(Amount);
        }
    }

    public class Item
    {
        public string Name { get; }
        public string Description { get; }
        public ItemType Type { get; }
        public bool IsEquiped { get; set; }
        public bool IsHave { get; set; }
        public int BuyGold { get; }
        public int SellGold { get; }
        public int Count { get; set; }

        public Item(string name, string description, ItemType type, bool isHave, int buyGold, int sellGold)
        {
            Name = name;
            Description = description;
            Type = type;
            IsHave = isHave;
            BuyGold = buyGold;
            SellGold = sellGold;
        }
    }
}
