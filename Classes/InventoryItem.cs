using baker_biz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.Classes
{
    public class InventoryItem : IInventoryItem
    {
        private string _name;
        private string _units;

        public InventoryItem(string name, string units)
        {
            _name = name;
            _units = units;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetUnits()
        {
            return _units;
        }
    }
}
