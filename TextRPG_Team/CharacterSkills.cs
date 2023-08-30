using System;

namespace TextRPG_Team
{
    public class CharacterSkills
    {
        public void AttackSigleTarget(Monster target, float damage, float damageMod)
        {
            damage *= damageMod;
            target.CurrentHp -= (int)damage;
        }

        public void AttackMutipleTarget(Monster[] targets, string damageMessage, float damage, float damageMod, int targetCount = 2)
        {
            List<Monster> newTargets = new List<Monster>(targets);
            targetCount = targets.Length < targetCount ? targets.Length : targetCount;
            damage *= damageMod;
            int count = 0;
            while (count < targetCount)
            {
                int index = Utility.rand.Next(0, count);
                targets[index].CurrentHp -= (int)damage;
                newTargets.Remove(newTargets[index]);
                Console.WriteLine($"{targets[index].Name}을(를) 맞췄습니다. {damageMessage}");
                count++;
            }
        }
    }
}