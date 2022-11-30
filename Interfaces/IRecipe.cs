using baker_biz.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.Interfaces
{
    public interface IRecipe
    {
        string GetName();

        List<RecipeIngredient> GetIngredientList();
    }
}
