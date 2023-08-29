public class Monster
{
    public string Name { get; }
    public int Level { get; }
    public int MaxHp { get; }
    public int CurrentHp { get; set; }
    public int Atk { get; }

    public bool IsDead => CurrentHp <= 0;

    public Monster(string name, int level, int maxHp, int attack)
    {
        Name = name;
        Level = level;
        MaxHp = maxHp;
        CurrentHp = maxHp;
        Atk = attack;
    }
    
    public Monster Clone()
    {
        return new Monster(Name, Level, MaxHp, Atk);
    }
}