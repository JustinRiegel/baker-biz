using baker_biz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.Classes
{
    public class RecipeIngredient : IRecipeIngredient, IInventoryItem
    {
        private string _name;
        private int _cost;
        private string _units;

        public RecipeIngredient(string name, int cost, string units)
        {
            _name = name;
            _cost = cost;
            _units = units;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetCost()
        {
            return _cost;
        }

        public string GetUnits()
        {
            return _units;
        }

        public override string ToString()
        {
            if(!string.IsNullOrEmpty(_units))
            {
                return $"{_cost} {_units} of {_name}";
            }
            else
            {
                return $"{_cost} {_name}";
            }
        }
    }
}
