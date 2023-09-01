namespace TextRPG_Team
{
    public enum SkillType
    {
        SigleTarget,
        MultipleTarget
    }

    public class Skill
    {
        public string Name { get; }
        public string Description { get; }
        public SkillType Type { get; }
        public int Cost { get; }
        public float DamageMod { get; }
        public int TargetCount { get; }

        // 단일 타겟 스킬과 다중 타겟 스킬을 생성자로 나눴다.
        
        // 배틀 시스템 들어갔을 때 type에 따라 캐스팅하여 UseSkill 호출하면 됨

        // targetCount == 0 은 전체 공격으로 설정
        public Skill(string name, string description, SkillType type, int cost, float damageMod, int targetCount = 1)
        {
            Name = name;
            Cost = cost;
            Type = type;
            DamageMod = damageMod;
            Description = description;
            TargetCount = targetCount;
        }
    }
}
