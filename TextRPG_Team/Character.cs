namespace TextRPG_Team
{
    public class Character
    {
        public List<Skill> Skills { get; set; }

        public string Name { get; set; }
        public string Job { get; }
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int MaxHp { get; }
        public int CurrentHp { get; set; }
        public int CurrentMp { get; private set; }
        public int MaxMp { get; }
        public int Gold { get; }
        public bool IsDead => CurrentHp <= 0;

        public int Exp { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int mp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            MaxHp = hp;
            CurrentHp = hp;
            CurrentMp = mp;
            MaxMp = mp;
            CurrentMp = mp;
            Gold = gold;
            Exp = 0;
        }

        public void HealHP(int amount)
        {
            CurrentHp = Math.Clamp(CurrentHp + amount, 0, MaxHp);
        }

        public void HealMP(int amount)
        {
            CurrentMp = Math.Clamp(CurrentMp + amount, 0, MaxMp);
        }
    }
}
