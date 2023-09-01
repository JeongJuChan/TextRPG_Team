namespace TextRPG_Team
{
    public class CharacterSkills
    {
        public void AttackSigleTarget(Monster target, float damage, float damageMod)
        {
            damage *= damageMod;
            target.CurrentHp -= (int)damage;
        }

        public void AttackMutipleTarget(List<Monster> aliveMonsters, List<Monster> killedMonster, string damageMessage, float damage, float damageMod, int targetCount = 2)
        {
            List<Monster> newTargets = new List<Monster>(aliveMonsters);
            targetCount = newTargets.Count < targetCount ? newTargets.Count : targetCount;
            damage *= damageMod;

            int count = 0;
            
            // targetCount가 0일시 전체공격
            targetCount = targetCount == 0 ? aliveMonsters.Count : targetCount;

            while (count < targetCount)
            {
                int index = Utility.rand.Next(0, count);
                newTargets[index].CurrentHp -= (int)damage;
                Console.WriteLine($"{newTargets[index].Name}을(를) 맞췄습니다. {damageMessage}");
                if (newTargets[index].IsDead)
                {
                    Console.WriteLine($"{newTargets[index].Name}");
                    Console.WriteLine("HP 0 -> Dead");
                    killedMonster.Add(newTargets[index]);
                    aliveMonsters.Remove(newTargets[index]);
                }

                newTargets.Remove(newTargets[index]);
                count++;
            }
        }
    }
}