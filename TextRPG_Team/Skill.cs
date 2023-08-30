namespace TextRPG_Team
{
    public enum SkillType
    {
        SigleTarget,
        MultipleTarget
    }

    public class SingleSkill : Skill
    {
        public Action<Monster, float, float> SingleAction { get; }

        public SingleSkill(string name, string description, int cost, float damageMod, Action<Monster, float, float> singleAction) 
            : base(name, description, SkillType.SigleTarget, cost, damageMod)
        {
            SingleAction = singleAction;
        }

        public void UseSkill(Monster target, int damage)
        {
            SingleAction?.Invoke(target, damage, DamageMod);
        }
    }

    public class MultipleSkill : Skill
    {
        public int TargetCount { get; }
        public Action<Monster[], string, float, float, int> MultipleAction { get; }

        public MultipleSkill(string name, string description, int cost, float damageMod, Action<Monster[], string, float, float, int> multipleAction, int targetCount = int.MaxValue) 
            : base(name, description, SkillType.MultipleTarget, cost, damageMod)
        {
            TargetCount = targetCount;
            MultipleAction = multipleAction;
        }

        public void UseSkill(Monster[] targets, string damageMessage, int damage)
        {
            MultipleAction?.Invoke(targets, damageMessage, damage, DamageMod, TargetCount);
        }
    }

    public class Skill
    {
        public string Name { get; }
        public SkillType Type { get; }
        public int Cost { get; }
        public float DamageMod { get; }
        public string Description { get; }

        // 단일 타겟 스킬과 다중 타겟 스킬을 생성자로 나눴다.
        
        // 배틀 시스템 들어갔을 때 type에 따라 캐스팅하여 UseSkill 호출하면 됨

        public Skill(string name, string description, SkillType type, int cost, float damageMod)
        {
            Name = name;
            Cost = cost;
            Type = type;
            DamageMod = damageMod;
            Description = description;
        }

    }
}
