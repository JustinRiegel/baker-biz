using baker_biz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.Classes
{
    public class CinnamonTopping : RecipeIngredientDecorator
    {
        public CinnamonTopping(IRecipe recipe) : base(recipe)
        {

        }

        public override string GetName()
        {
            return base.GetName() + " with Cinnamon topping";
        }

        public override List<RecipeIngredient> GetIngredientList()
        {
            var returnList = new List<RecipeIngredient>(base.GetIngredientList());
            var cinnamonIngredient = new RecipeIngredient(Constants.CINNAMON, 1, Constants.TBSP);
            returnList.Add(cinnamonIngredient);

            return returnList;
        }
    }
}
