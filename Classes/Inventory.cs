using baker_biz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.Classes
{
    public class Inventory : IInventory
    {
        Dictionary<string, int> _inventory = new Dictionary<string, int>();

        public Inventory()
        {

        }

        public void AddInventoryItem(IInventoryItem inventoryItem, int amount)
        {
            string itemName = inventoryItem.GetName();

            //check if the ingredient already has an entry in the inventory. if it exists, adjust the amount. otherwise, add it to the inventory with the appropriate amount
            if(_inventory.Any(i => i.Key == itemName))
            {
                //i am assuming that the amount passed to this function is always positive.
                //since this is not exposed to the user, and the amount entry is validated when the user inputs the amounts, its PROBABLY fine.
                //i could combine the Add and Remove functions into the same and just "add" or "remove" based on positive or negative values, but that is less readable
                _inventory[itemName] += amount;
            }
            else
            {
                _inventory.Add(itemName, amount);
            }
        }

        public bool RemoveInventoryItem(IInventoryItem inventoryItem, int amount)
        {
            string itemName = inventoryItem.GetName();

            //if the ingredient is in the inventory, attempt to remove the specified amount
            if (_inventory.Any(i => i.Key == itemName))
            {
                //if the amount requested to be removed would not result in negative amounts in the inventory, remove them
                if(_inventory[itemName] - amount >= 0)
                {
                    _inventory[itemName] -= amount;
                    return true;
                }
            }

            return false;
        }

        public int GetInventoryItemCount(IInventoryItem inventoryItem)
        {
            string itemName = inventoryItem.GetName();

            //if the ingredient is in the inventory, return the count
            if (_inventory.Any(i => i.Key == itemName))
            {
                return _inventory[itemName];
            }

            //if the ingredient doesn't exist in the inventory, the amount is obviously 0. i don't see a reason (yet) to make a distinction between null and 0 for inventory
            return 0;
        }

        public void ZeroOutInventory()
        {
            foreach(var item in _inventory)
            {
                _inventory[item.Key] = 0;
            }
        }

        public void ClearOutInventory()
        {
            _inventory.Clear();
        }
    }
}
