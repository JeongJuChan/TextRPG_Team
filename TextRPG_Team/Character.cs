using System.Reflection.Emit;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace TextRPG_Team
{
    public class Character
    {
        public List<Skill> Skills { get; set; }

        public string Name { get; set; }
        public string Job { get; }
        public int Level { get; set; }
        public int Atk { get; }
        public int Def { get; }
        public int MaxHp { get; }
        public int CurrentHp { get; set; }
        public int CurrentMp { get; private set; }
        public int MaxMp { get; }
        public int Gold { get; set; }
        public bool IsDead => CurrentHp <= 0;

        [JsonInclude]
        public int CurrentExp { get; set; }

        [JsonConstructor]
        public Character(string name, string job, int level, int atk, int def, int currenthp, int currentmp, int gold)
        {
            Skills = new List<Skill>();
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            MaxHp = currenthp;
            CurrentHp = currenthp;
            CurrentMp = currentmp;
            MaxMp = currentmp;
            Gold = gold;
            CurrentExp = 0;
        }

        public Character(Character org)
        {
            Skills = new List<Skill>();
            Name = org.Name;
            Job = org.Job;
            Level = org.Level;
            Atk = org.Atk;
            Def = org.Def;
            MaxHp = org.MaxHp;
            CurrentHp = org.CurrentHp;
            CurrentMp = org.CurrentMp;
            MaxMp = org.MaxMp;
            Gold = org.Gold;
            CurrentExp = org.CurrentExp;
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
