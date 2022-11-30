using baker_biz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.Classes
{
    public class RecipeIngredientDecorator : IRecipe
    {
        private IRecipe _recipe;

        public RecipeIngredientDecorator(IRecipe recipe)
        {
            _recipe = recipe;
        }

        public virtual string GetName()
        {
            return _recipe.GetName();
        }

        public virtual List<RecipeIngredient> GetIngredientList()
        {
            return _recipe.GetIngredientList();
        }
    }
}
