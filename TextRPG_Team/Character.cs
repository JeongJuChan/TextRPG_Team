
#region 데이터

public class Character
{
    public string Name { get; }
    public string Job { get; }
    public int Level { get; }
    public int Atk { get; }
    public int Def { get; }
    public int MaxHp { get; }
    public int CurrentHp { get; set; }
    public int Gold { get; }
    public bool IsDead => CurrentHp <= 0;

    public Character(string name, string job, int level, int atk, int def, int maxHp, int gold)
    {
        Name = name;
        Job = job;
        Level = level;
        Atk = atk;
        Def = def;
        MaxHp = maxHp;
        CurrentHp = maxHp;
        Gold = gold;
    }

    private static Random random = new Random();
    public int CalculateDamage()
    {
        double variation = 1.0 + (random.NextDouble() * 0.2 - 0.1); // 0.9 ~ 1.1 사이의 랜덤값
        double damage = Atk * variation;
        return (int)Math.Ceiling(damage);
    }
}

#endregion
