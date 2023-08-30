using TextRPG_Team;

public class Monster
{
    public string Name { get; }
    public int Level { get; }
    public int MaxHp { get; }
    public int CurrentHp { get; set; }
    public int Atk { get; }
    public int Gold { get; }
    public Item DropItem { get; set; }
    public bool IsDead => CurrentHp <= 0;

    public Monster(string name, int level, int maxHp, int attack, int gold, Item dropitem)
    {
        Name = name;
        Level = level;
        MaxHp = maxHp;
        CurrentHp = maxHp;
        Atk = attack;
        Gold = gold;
        DropItem = dropitem;
    }
    
    public Monster Clone()
    {
        return new Monster(Name, Level, MaxHp, Atk, Gold, DropItem);
    }
}