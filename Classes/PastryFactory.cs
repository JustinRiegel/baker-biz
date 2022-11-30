using baker_biz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.Classes
{
    public class PastryFactory
    {
        public IRecipe GetPie(string type)
        {
            switch(type)
            {
                case Constants.APPLES: return new ApplePie();
                default: return null;
            }
        }
    }
}
