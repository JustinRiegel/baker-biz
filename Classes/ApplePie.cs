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
            returnList.Add(new RecipeIngredient(Constants.APPLES, string.Empty, 1, string.Empty, 3));
            returnList.Add(new RecipeIngredient(Constants.SUGAR, Constants.POUNDS, 1, Constants.POUNDS, 2));
            returnList.Add(new RecipeIngredient(Constants.FLOUR, Constants.POUNDS, 1, Constants.POUNDS, 1));
            returnList.Add(new RecipeIngredient(Constants.BUTTER, Constants.STICKS, 8, Constants.TBSP, 6));//8 tbsp of butter per stick, 6 tbsp per pie

            return returnList;
        }
    }
}
