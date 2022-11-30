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
        public void AddInventoryItem(IInventoryItem inventoryItem, int amount);

        public bool RemoveInventoryItem(IInventoryItem inventoryItem, int amount);

        public int GetInventoryItemCount(IInventoryItem inventoryItem);

        public void ZeroOutInventory();

        public void ClearOutInventory();
    }
}
