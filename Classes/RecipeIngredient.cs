using baker_biz.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace baker_biz.Classes
{
    public class RecipeIngredient : IRecipeIngredient
    {
        public string Name { get; private set; }
        public string AskingUnits { get; private set; }
        public int StockingConversionMultiplier { get; private set; }
        public string MakingUnits { get; private set; }
        public int AmountUsed { get; private set; }

        public RecipeIngredient(string name, string askingUnits, int stockingConversionMultiplier, string makingUnits, int amountUsed)
        {
            Name = name;
            AskingUnits = askingUnits;
            StockingConversionMultiplier = stockingConversionMultiplier;
            MakingUnits = makingUnits;
            AmountUsed = amountUsed;
        }

        public string GetName()
        {
            return Name;
        }

        public string GetAskingUnits()
        {
            return AskingUnits;
        }

        public int GetStockingConversionMultiplier()
        {
            //convert from the units the user input the amount as, to what it is stored as in the inventory
            //i.e. the user is asked how many whole sticks of butter they have, but butter is used (and thus stored) in tablespoons.
            //     this multiplier is how you get from sticks to tbsp, which is 8 tbsp per stick
            return StockingConversionMultiplier;
        }

        public int GetAmountUsed()
        {
            return AmountUsed;
        }

        public string GetMakingUnits()
        {
            return MakingUnits;
        }

        public override string ToString()
        {
            if(!string.IsNullOrEmpty(MakingUnits))
            {
                return $"{AmountUsed} {MakingUnits} of {Name}";
            }
            else
            {
                return $"{AmountUsed} {Name}";
            }
        }
    }
}
