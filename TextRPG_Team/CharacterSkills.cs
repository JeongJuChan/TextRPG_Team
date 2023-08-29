﻿namespace TextRPG_Team
{
    public class CharacterSkills
    {
        public void AttackSigleTarget(string target, float damage, float damageMod)
        {
            damage *= damageMod;
            //target.TakeDamage(damage);
        }

        public void AttackMutipleTarget(List<string> targets, float damage, float damageMod, int targetCount = 2)
        {
            List<string> newTarget = new List<string>(targets);
            damage *= damageMod;
            int count = 0;
            while (count < newTarget.Count)
            {
                int index = Utility.rand.Next(0, count);
                //targets[index].TakeDamage(damage);
                newTarget.Remove(newTarget[index]);
                count++;
            }
        }
    }
}