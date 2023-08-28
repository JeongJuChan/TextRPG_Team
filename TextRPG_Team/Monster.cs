public class Monster
{
    public string Name { get; }
    public int Level { get; }
    public int Atk { get; }
    public int Hp { get; set; }
    public bool IsDead => Hp <= 0;

    public Monster(string name, int level, int atk, int hp)
    {
        Name = name;
        Level = level;
        Atk = atk;
        Hp = hp;
    }
}