using System;


namespace BullsAndCowsConsole
{
    class Program
    {
        static void ShowIntro()
        {
            Console.WriteLine("Welcome to Bulls and Cows.");
            Console.WriteLine("For tutorial how to play the game press F1.");
            Console.WriteLine("Press any key to continue.");

            if (Console.ReadKey().Key == ConsoleKey.F1)
            {
                Console.WriteLine("\nOne player, the Chooser(me), thinks of a four-digit number and the other player, the Guesser(you), tries to guess it.\n\n" +
                    "At each turn the Guesser tries a four-digit number, and the Chooser says how close it is to the answer by giving:\n" +
                    "The number of Bulls - correct digits in the right position.\n" +
                    "The number of Cows - correct digits but in the wrong position.\n\n" +
                    "The Guesser tries to guess the answer in the fewest number of turns.\n" +
                    "The four-digit number can start with 0 and dublicates are not allowed.\n\n" +
                    "Now give it a try.\n");
            }
        }

        static bool IsNumber(string str)
        {
            int q = str.Length;

            for (int i = 0; i < q; i++)
            {
                if (str[i] >= '0' && str[i] <= '9')
                {
                    return true;
                }
            }
            return false;
        }

        static bool ContainmentCheck(int input, int[] existingArray)
        {
            int lengthArray = existingArray.Length;
            for (int i = 0; i < lengthArray; i++)
            {
                if (existingArray[i] == input)
                {
                    return true;
                }
            }
            return false;
        }

        static int[] GenerateProblem()
        {
            int randomInt;
            int[] generatedProblem = { };
            while (generatedProblem.Length < 4)
            {
                int len = generatedProblem.Length;
                randomInt = new Random().Next(9);

                if (!ContainmentCheck(randomInt, generatedProblem))
                {
                    Array.Resize(ref generatedProblem, len + 1);
                    generatedProblem[len] = randomInt;
                }
            }
            return generatedProblem;
        }

        static int[] StringToIntArray(string input)
        {
            int len = input.Length;
            int[] distributedDigits = new int[len];
            for (int i = 0; i < len; i++)
            {
                distributedDigits[i] = input[i] - 48; //48 is the char value of 0
            }
            return distributedDigits;
        }

        static bool CheckGuess(string toCheckGuessString, int[] toCheckProblem)
        {
            int bulls = 0;
            int cows = 0;

            int[] toCheckGuess = StringToIntArray(toCheckGuessString);                    
            
            for (int i = 0 ; i < 4; i++)
            {
                if (toCheckGuess[i] == toCheckProblem[i])
                    bulls++;
            }

            if (bulls == 4)
            {
                return true;
            }
            for (int i = 0; i < 4; i++)
            {
                if (ContainmentCheck(toCheckGuess[i], toCheckProblem))
                {
                    cows++;
                }
            }

            cows -= bulls;
            Console.WriteLine("Bulls: " + bulls + " and Cows: " + cows);
            return false;
        }

        static string PromptGuess()
        {
            string guessString = Console.ReadLine();

            while (guessString.Length != 4 | IsNumber(guessString) == false)
            {

                Console.WriteLine("Please enter 4 digits.");
                guessString = Console.ReadLine();

            }
            return guessString;
        }

        static void GameAftermath(int turns)
        {
            if (turns == 1)
            {
                Console.WriteLine("Fat chance! You guessed my number from the first try.");
            }

            else
            {
                Console.WriteLine("Good job! You guessed my number with " + turns + " guesses.");

                if (turns < 4)
                {
                    Console.WriteLine("That's very impressive!");
                }
                else if (turns > 8)
                {
                    Console.WriteLine("Hint for the next game - start thinking...");
                }
            }
            Console.WriteLine("Press any key to restart, press Esc to close.");

            var finalChoice = Console.ReadKey();
            Console.Clear();
            if (finalChoice.Key == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }

        }

        static void Main(string[] args)
        {
            ShowIntro();

            for (; ; )
            {
                int[] correctDigits = GenerateProblem();
                int turnsPlayed = 1;
                Console.WriteLine("I have created a random 4 digits number. Try to guess it.");

                for (; ; )
                {

                    Console.WriteLine("Write your guess.");
                    string validGuessString = PromptGuess();

                    if (CheckGuess(validGuessString, correctDigits))
                    {
                        break;
                    }
                    else
                    {
                        turnsPlayed++;
                    }
                }
                GameAftermath(turnsPlayed);
            }
        }
    }
}
