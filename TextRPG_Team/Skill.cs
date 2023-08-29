namespace TextRPG_Team
{
    public enum SkillType
    {
        SigleTarget,
        MultipleTarget
    }

    public class SigleSkill : Skill
    {
        Action<string, float, float> skill;

        public SigleSkill(string name, string description, int cost, float damage, float damageMod, Action<string, float, float> skillAction) : base(name, description, SkillType.SigleTarget, cost, damage, damageMod)
        {
            skill = skillAction;
        }

        public void UseSkill(string target)
        {
            skill?.Invoke(target, Damage, DamageMod);
        }
    }

    public class MultipleSkill : Skill
    {
        Action<List<string>, float, float, int> mutipleSkill;

        public MultipleSkill(string name, string description, int cost, float damage, float damageMod, Action<List<string>, float, float, int> mutipleSkill) : base(name, description, SkillType.SigleTarget, cost, damage, damageMod)
        {
            this.mutipleSkill = mutipleSkill;
        }

        public void UseSkill(List<string> targets, int targetCount = 2)
        {
            mutipleSkill?.Invoke(targets, Damage, DamageMod, targetCount);
        }
    }

    public class Skill
    {
        // 정보 MP 추가
        // 캐릭터에서 가져오기
        // 2. 스킬 항목 추가
        
        // TODO :
        // Program에 있는 배틀 로직 배틀 쪽에 붙이기

        public string Name { get; }
        public SkillType Type { get; }
        public int Cost { get; }
        public float Damage { get; }
        public float DamageMod { get; }
        public string Description { get; }

        // 단일 타겟 스킬과 다중 타겟 스킬을 생성자로 나눴다.
        
        // 배틀 시스템 들어갔을 때 type에 따라 캐스팅하여 UseSkill 호출하면 됨

        public Skill(string name, string description, SkillType type, int cost, float damage, float damageMod)
        {
            Name = name;
            Cost = cost;
            Damage = damage;
            DamageMod = damageMod;
            Description = description;
        }

    }
}
