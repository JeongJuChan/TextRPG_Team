using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_Team.Table
{
    public class ExpTable
    {
        public int id { get; set; }
        public int NeedEXP { get; set; }
        public int StackEXP { get; set; }
    }

    public class DropTable
    {
        public int id { get; set; }
        public string Item { get; set; } = string.Empty;
        public float Rate { get; set; }
    }

}
