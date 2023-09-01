
namespace TextRPG_Team
{
    public enum ItemType
    {
        Equipment,
        Consumable,
    }

    public class Item
    {
        public int Id { get; set; }
        public string Name { get; }
        public string Description { get; }
        public ItemType Type { get; }
        public bool IsHave { get; set; }
        public int BuyGold { get; }
        public int SellGold { get; }

        public class Equipment
        {
            public int Atk { get; }
            public int Def { get; }
            public bool IsEquiped { get; set; }

            public Equipment(int atk, int def, bool isEquiped)
            {
                Atk = atk;
                Def = def;
                IsEquiped = isEquiped;
            }
        }

        public class Consumable
        {
            public int Count { get; set; }
            public int Amount { get; }

            public Consumable(int count, int amount)
            {
                Count = count;
                Amount = amount;
            }
        }

        public Equipment EquipmentData { get; set; }
        public Consumable ConsumableData { get; set; }

        public Item(int id, string name, string description, ItemType type, bool isHave, int buyGold, int sellGold)
        {
            Id = id;
            Name = name;
            Description = description;
            Type = type;
            IsHave = isHave;
            BuyGold = buyGold;
            SellGold = sellGold;
        }
    }
}
