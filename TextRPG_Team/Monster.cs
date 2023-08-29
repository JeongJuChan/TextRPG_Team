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
    Random random = new Random();
    public int CalculateDamage()
    {
        double variation = 1.0 + (random.NextDouble() * 0.2 - 0.1); // 0.9 ~ 1.1 사이의 랜덤값
        double damage = Atk * variation;
        return (int)Math.Ceiling(damage);
    }
    public Monster Clone()
    {
        return new Monster(Name, Level, MaxHp, Atk);
    }
}