using baker_biz.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.Interfaces
{
    public interface IInventory
    {
        public void AddInventoryItem(IRecipeIngredient ingredient, int amount);

        public bool RemoveInventoryItem(IRecipeIngredient ingredient, int amount);

        public int GetInventoryItemCount(IRecipeIngredient ingredient);

        public void ZeroOutInventory();

        public void ClearOutInventory();
    }
}
