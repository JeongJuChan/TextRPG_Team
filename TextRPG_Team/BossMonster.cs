using TextRPG_Team;

public class BossMonster : Monster
{
    public bool IsBoss { get; }
    public BossMonster(string name, int level, int maxHp, int attack, int gold, Item dropItem)
        : base(name, level, maxHp, attack, gold, dropItem)
    {
        IsBoss = true;
    }

    // 기존 Clone 메서드를 override하여 BossMonster를 반환하도록 변경
    public override Monster Clone()
    {
        return new BossMonster(Name, Level, MaxHp, Atk, Gold, DropItem);
    }
}