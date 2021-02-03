using System;
using System.Linq;


namespace BullsAndCowsConsole
{
    class Program
    {
        static void Intro()
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

        static bool IsDigitsOnly(string str)
        {
            int q = str.Length;

            for (int i = 0; i < q; i++)
            {
                if (Convert.ToByte(str[i]) < 48 | Convert.ToByte(str[i]) > 57)
                /*  48 is the Byte value of 0
                    57 is the Byte value of 9*/
                {
                    return false;
                }
            }
            return true;
        }

        static int[] GenerateAnswer()
        {
            int randomInt;
            int[] generatedAnswer = new int[4];

            for (int i = 0; i < 4; i++)
            {
                randomInt = new Random().Next(9);
                if (!generatedAnswer.Contains(randomInt))
                    generatedAnswer[i] = randomInt;
                else
                    i--;
            }
            return generatedAnswer;
        }

        static bool CheckGuess2(string toCheckGuessString, int[] toCheckAnswer)
        {
            int bulls = 0;
            int cows = 0;
            int toCheckGuessFull = Convert.ToInt32(toCheckGuessString);
            int[] toCheckGuess = new int[4];

            toCheckGuess[0] = (toCheckGuessFull / 1000);
            toCheckGuess[1] = (toCheckGuessFull / 100) % 10;
            toCheckGuess[2] = (toCheckGuessFull / 10) % 10;
            toCheckGuess[3] = toCheckGuessFull % 10;
            
            for (int i = 0 ; i < 4; i++)
            {
                if (toCheckGuess[i] == toCheckAnswer[i])
                    bulls++;
            }

            if (bulls == 4)
            {
                return true;
            }
            else
                for (int i = 0; i < 4; i++)
                {
                    if (toCheckAnswer.Contains(toCheckGuess[i]))
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

            while (guessString.Length != 4 | IsDigitsOnly(guessString) == false)
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
                Console.WriteLine("Fat chance! Go guessed my number from the first try.");
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
            Intro();

            for (; ; )
            {
                int[] arrayAnswer = GenerateAnswer();
                int turnsPlayed = 1;
                Console.WriteLine("I have created a random 4 digits number. Try to guess it.");

                for (; ; )
                {

                    Console.WriteLine("Write your guess.");
                    string validGuessString = PromptGuess();

                    if (CheckGuess2(validGuessString, arrayAnswer))
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
