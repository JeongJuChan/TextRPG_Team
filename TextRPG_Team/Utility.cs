using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team
{
    public class Utility
    {
        public static Random rand = new Random();

        #region Skills

        #region Single
        public static void AlphaStrike(string target, float damage)
        {
            damage *= 2;
            //target.TakeDamage(damage);
        }
        #endregion

        #region Multiple
        public static void DoubleStrike(List<string> targets, float damage) 
        {
            int targetCount = 0;

            damage *= 1.5f;

            while (targetCount < 2)
            {
                int index = rand.Next(0, targetCount);
                //targets[index].TakeDamage(damage);
                targetCount++;
            }
            
        }
        #endregion

        #endregion

    }
}
