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
        public int Hp { get; }
        public int Mp { get; }
        public int Gold { get; }

        public int Exp { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int mp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Mp = mp;
            Gold = gold;
            Exp = 0;
        }
    }

}