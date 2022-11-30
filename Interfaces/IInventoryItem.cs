using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.Interfaces
{
    public interface IInventoryItem
    {
        public string GetName();

        public string GetUnits();
    }
}
