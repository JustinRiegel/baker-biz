using baker_biz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.Classes
{
    public class ApplePie : IRecipe
    {
        public ApplePie()
        {

        }

        public string GetName()
        {
            return "Apple Pie";
        }

        List<RecipeIngredient> IRecipe.GetIngredientList()
        {
            var returnList = new List<RecipeIngredient>();
            returnList.Add(new RecipeIngredient(Constants.APPLES, 3, string.Empty));
            returnList.Add(new RecipeIngredient(Constants.SUGAR, 2, Constants.POUNDS));
            returnList.Add(new RecipeIngredient(Constants.FLOUR, 1, Constants.POUNDS));

            return returnList;
        }
    }
}
