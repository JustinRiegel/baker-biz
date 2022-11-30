﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.Interfaces
{
    public interface IRecipeIngredient
    {
        //TODO int is fine here until we encounter recipes with differing units, i.e. cups and ounces
        public int GetCost();

        public string ToString();
    }
}
