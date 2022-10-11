namespace Bakery
{
    class Program
    {
        //global variables, very scary! just in time for spooky season. these will eventually get rolled into an appropriate class
        const int APPLES_PER_PIE = 3;
        const int POUNDS_SUGAR_PER_PIE = 2;
        const int POUNDS_FLOUR_PER_PIE = 1;

        static void Main()
        {
            // want to maximize the number of apple pies we can make.
            // it takes 3 apples, 2 lbs of sugar and 1 pound of flour to make 1 apple pie
            // this is intended to run on .NET Core
            char userSelection = ' ';
            do
            {
                var numApples = GetUserInputPositiveInteger("How many apples do you have?");
                var poundsSugar = GetUserInputPositiveInteger("How many pounds of sugar do you have?");
                var poundsFlour = GetUserInputPositiveInteger("How many pounds of flour do you have?");

                var pieCount = CalculatePieCount(numApples, poundsSugar, poundsFlour);
                if (pieCount >= 0)
                {
                    Console.WriteLine($"{Environment.NewLine}With:{Environment.NewLine}" +
                        $"{numApples} apples{Environment.NewLine}" +
                        $"{poundsSugar} pounds of sugar{Environment.NewLine}" +
                        $"{poundsFlour} pounds of flour{Environment.NewLine}" +
                        $"you can make {pieCount} pies.{Environment.NewLine}");
                    CalculateLeftoverIngredients(pieCount, numApples, poundsSugar, poundsFlour);
                }
                Console.WriteLine($"-------------------{Environment.NewLine}");
                userSelection = GetUserInputAlphabetCharacter("Press 'c' to calculate again, or press 'q' to quit!", 'c', 'q');
                //dump a blank line into the console for readability and spacing
                Console.WriteLine("");
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

        //using the constants and the user input for ingredient amounts, calculate how many pies can be made
        static int CalculatePieCount(int numApples, int poundsSugar, int poundsFlour)
        {
            try
            {
                var maxPiesFromApples = numApples / APPLES_PER_PIE;
                var maxPiesFromSugar = poundsSugar / POUNDS_SUGAR_PER_PIE;
                var maxPiesFromFlour = poundsFlour / POUNDS_FLOUR_PER_PIE;
                //get the smallest of the 3 maxes, as that will be the maximum number of pies that can be made
                return Math.Min(Math.Min(maxPiesFromApples, maxPiesFromSugar), maxPiesFromFlour);
            }
            catch(DivideByZeroException e)
            {
                Console.WriteLine("You generated a divide-by-zero error during calculation, which is only possible if you changed the constants in the code. You did this to yourself.");
                return -1;
            }
            catch(Exception e)
            {
                Console.WriteLine($"The calculation encountered an error: {e.Message}.");
                return -1;
            }
        }

        //using the pie count and the user input for ingredient amounts, calculate and print the leftover ingredients
        //i am printing the leftovers within this function because the current alternative is to have a version that is just doing the math and returning the value
        //and at that point, because this math is so simple, you could just do the math instead of calling the function.
        //instead, i chose to use this function to better encapsulate the logic, though the long-term plan is to fold this functionality into a class
        static void CalculateLeftoverIngredients(int pieCount, int numApples, int poundsSugar, int poundsFlour)
        {
            var leftoverApples = numApples - (pieCount * APPLES_PER_PIE);
            var leftoverSugar = poundsSugar - (pieCount * POUNDS_SUGAR_PER_PIE);
            var leftoverFlour = poundsFlour - (pieCount * POUNDS_FLOUR_PER_PIE);

            Console.WriteLine($"You will have the following ingredient amounts left over:{Environment.NewLine}" +
                $"{leftoverApples} apples{Environment.NewLine}" +
                $"{leftoverSugar} pounds of sugar{Environment.NewLine}" +
                $"{leftoverFlour} pounds of flour");
        }
    }
}
