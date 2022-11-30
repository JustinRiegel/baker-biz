using baker_biz.Classes;
using baker_biz.Interfaces;

namespace Bakery
{
    public class BakeryCalculator
    {
        private static PastryFactory _pastryFactory = new PastryFactory();
        private static Inventory _inventory = new Inventory();

        //possible future things to look out for:
        //--user adding recipes
        //--additional bakery locations (i.e. multiple inventories)
        //--various units for ingredients
        //--being able to make a select amount of select recipes
        //--shopping list of what's missing for a given amount of a reciple(s)

        static void Main()
        {
            //TODO some kind of priority for recipes
            List<IRecipe> recipeList = new List<IRecipe>();
            IRecipe applePie = _pastryFactory.GetPie(Constants.APPLES);
            RecipeIngredientDecorator applePieDecorator = new RecipeIngredientDecorator(applePie);
            IRecipe applePieWithCinnamon = new CinnamonTopping(applePieDecorator);
            //kinda hacky, but for now just attempt to make each recipe in the list in order
            recipeList.Add(applePieWithCinnamon);
            recipeList.Add(applePie);

            List<IInventoryItem> inventoryItemList = new List<IInventoryItem>();
            //get each unique ingredient from the list of recipes to query the user for their amounts
            foreach(var recipe in recipeList)
            {
                foreach(var ingredient in recipe.GetIngredientList())
                {
                    if(!inventoryItemList.Any(i => i.GetName() == ingredient.GetName()))
                    {
                        inventoryItemList.Add(ingredient);
                    }
                }
            }

            //loop for user input
            char userSelection = ' ';
            do
            {
                //clear out anything in the inventory so nothing lingers between calculations.
                _inventory.ClearOutInventory();

                //query the user for current inventory status
                foreach(var item in inventoryItemList)
                {
                    var queryString = "";
                    if(string.IsNullOrEmpty(item.GetUnits()))
                    {
                        queryString = $"How many {item.GetName()} do you have?";
                    }
                    else
                    {
                        queryString = $"How many {item.GetUnits()} of {item.GetName()} do you have?";
                    }
                    var itemCount = GetUserInputPositiveInteger(queryString);

                    _inventory.AddInventoryItem(item, itemCount);
                }

                //for each recipe, calculate how many can be made, report that to the user, then remove the specified amount of ingredients from the inventory
                foreach(var recipe in recipeList)
                {
                    var recipeCount = CalculateRecipeCount(recipe);
                    if (recipeCount >= 0)
                    {
                        Console.WriteLine($"You can make \"{recipe.GetName()}\" {recipeCount} times.");
                    }
                    RemoveRecipeIngredientsFromInventory(recipe, recipeCount);
                }

                //report the amount of inventory left over
                Console.WriteLine("You will have the following ingredients left over:");
                foreach(var item in inventoryItemList)
                {
                    if (!string.IsNullOrEmpty(item.GetUnits()))
                    {
                        Console.WriteLine($"{_inventory.GetInventoryItemCount(item)} {item.GetUnits()} of {item.GetName()}");
                    }
                    else
                    {
                        Console.WriteLine($"{_inventory.GetInventoryItemCount(item)} {item.GetName()}");
                    }
                }
                Console.Write(Environment.NewLine);

                userSelection = GetUserInputAlphabetCharacter("Press 'c' to calculate again, or press 'q' to quit!", 'c', 'q');
                //dump a blank line into the console for readability and spacing
                Console.Write(Environment.NewLine);
            } while (userSelection != 'q');

        }

        //get a single character of user input that must be a letter
        static char GetUserInputAlphabetCharacter(string userPrompt, params char[] validInputs)
        {
            Console.WriteLine(userPrompt);
            char input = char.ToLower(Console.ReadKey().KeyChar);
            
            //loop the input prompt while the user provides invalid values
            while (!char.IsLetter(input) || !validInputs.Contains(input))
            {
                Console.WriteLine($"{Environment.NewLine}The option you selected was invalid. Please select a valid option.");
                Console.WriteLine(userPrompt);
                input = char.ToLower(Console.ReadKey().KeyChar);
            }

            return input;
        }

        //get an integer of user input that must be a positive (or zero) integer
        static int GetUserInputPositiveInteger(string userPrompt)
        {
            Console.WriteLine(userPrompt);
            var input = Console.ReadLine();
            int numberInput;

            //loop the input prompt while the user provides invalid values
            while (!int.TryParse(input, out numberInput) || numberInput < 0)
            {
                Console.WriteLine("The number you entered was invalid. Please enter a positive whole number.");
                Console.WriteLine(userPrompt);
                input = Console.ReadLine();
            }

            return numberInput;
        }

        //calculate how many of a given recipe can be made with the current inventory
        static int CalculateRecipeCount(IRecipe recipe)
        {
            //we need mxaRecipecount to be int.MaxValue so we will always get a smaller value during calculation
            int maxRecipeCount = int.MaxValue;
            int maxRecipesFromIngredient;

            //loop the list of ingredients to find the maximum recipe count
            foreach(var ingredient in recipe.GetIngredientList())
            {
                maxRecipesFromIngredient = 0;
                try
                {
                    maxRecipesFromIngredient = _inventory.GetInventoryItemCount(ingredient) / ingredient.GetCost();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"The calculation encountered an error: {e.Message}.");
                    return -1;
                }

                //find out if we have a new, lower, maximum and store it if we do
                maxRecipeCount = Math.Min(maxRecipeCount, maxRecipesFromIngredient);
            }

            return maxRecipeCount;
        }

        static void RemoveRecipeIngredientsFromInventory(IRecipe recipe, int recipeCount)
        {
            //now that we know how many of the recipe to make, remove the ingredients from the inventory
            foreach (var ingredient in recipe.GetIngredientList())
            {
                //TODO see below note
                //there is a potential issue here if, for some reason, we cannot remove the specified amount of ingredients from the inventory.
                //this should never happen as we've done the calculations for this above. however.
                //if an issue ever does occur, the inventory will likely be in a bad state because we will probably have removed some, but not all, of the ingredients.
                //this will throw off the calculation of other recipes. there are solutions for this, but that is a problem for another day
                if (!_inventory.RemoveInventoryItem(ingredient, ingredient.GetCost() * recipeCount))
                {
                    Console.WriteLine($"There was a discrepancy between recipe calculation and inventory amounts.");
                    return;
                }
            }
        }
    }
}
