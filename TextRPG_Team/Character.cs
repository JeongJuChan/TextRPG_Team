namespace TextRPG_Team
{

    public class Character
    {
        public List<Skill> Skills { get; private set; } = new List<Skill>();

        public string Name { get; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; private set; }
        public int MaxHp { get; }
        public int Mp { get; private set; }
        public int MaxMp { get; }
        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int mp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            MaxHp = hp;
            Mp = mp;
            MaxMp = mp;
            Gold = gold;
        }

        public void HealHP(int amount)
        {
            Hp = Math.Clamp(Hp + amount, 0, MaxHp);
        }

        public void HealMP(int amount)
        {
            Mp = Math.Clamp(Mp + amount, 0, MaxMp);
        }
    }

}