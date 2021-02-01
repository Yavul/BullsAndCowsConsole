using System;

namespace BullsAndCowsConsole
{
    class Program
    {
        static public int GetNumber()
        {
            int randomInt = new Random().Next(9);
            return randomInt;
        }

        static bool isDigitsOnly(string str)
        {
            try
            {
                int num = Convert.ToInt32(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        static int[] generateSolution2() // NOT USED
        {
            int[] sol = new int[4];

            sol[0] = GetNumber();
            do
            {
                sol[1] = GetNumber();
            }
            while (sol[1] == sol[0]);
            do
            {
                sol[2] = GetNumber();
            }
            while (sol[2] == sol[0] | sol[2] == sol[1]);
            do
            {
                sol[3] = GetNumber();
            }
            while (sol[3] == sol[0] | sol[3] == sol[1] | sol[3] == sol[2]);
            return sol;
        }


        static int generateSolution()
        {
            int A;
            do
            {
                A = GetNumber();
            }
            while (A == 0);
            int B;
            do
            {
                B = GetNumber();
            }
            while (A == B);
            int C;
            do
            {
                C = GetNumber();
            }
            while (C == A | C == B);
            int D;
            do
            {
                D = GetNumber();
            }
            while (D == A | D == B | D == C);
            int solution = A * 1000 + B * 100 + C * 10 + D;
            return solution;
        }

        static void checkAnswer(string input, int toCheckSolution)
        {
            int bulls = 0;
            int cows = 0;

            int solutionDig1 = (toCheckSolution / 1000);
            int solutionDig2 = (toCheckSolution / 100) % 10;
            int solutionDig3 = (toCheckSolution / 10) % 10;
            int solutionDig4 = toCheckSolution % 10;

            int guessSolution = Convert.ToInt32(input);
            int Dig1 = (guessSolution / 1000);
            int Dig2 = (guessSolution / 100) % 10;
            int Dig3 = (guessSolution / 10) % 10;
            int Dig4 = guessSolution % 10;


            if (Dig1 == solutionDig1) { bulls++; }
            if (Dig2 == solutionDig2) { bulls++; }
            if (Dig3 == solutionDig3) { bulls++; }
            if (Dig4 == solutionDig4) { bulls++; }

            if (Dig1 == solutionDig2) { cows++; }
            else if (Dig1 == solutionDig3) { cows++; }
            else if (Dig1 == solutionDig4) { cows++; }

            if (Dig2 == solutionDig1) { cows++; }
            else if (Dig2 == solutionDig3) { cows++; }
            else if (Dig2 == solutionDig4) { cows++; }

            if (Dig3 == solutionDig1) { cows++; }
            else if (Dig3 == solutionDig2) { cows++; }
            else if (Dig3 == solutionDig4) { cows++; }

            if (Dig4 == solutionDig1) { cows++; }
            else if (Dig4 == solutionDig2) { cows++; }
            else if (Dig4 == solutionDig3) { cows++; }


            Console.WriteLine("Bulls: " + bulls + " and Cows: " + cows);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bulls and Cows.");
            for ( ; ; )
            {
                int turns = 1;
                int Solution = generateSolution();
                Console.WriteLine("I have created a random 4 digits number. Try to guess it.");

                for ( ; ; )
                {

                    Console.WriteLine("Write your guess.");
                    string guessSolutionString = Console.ReadLine();

                    while (guessSolutionString.Length != 4 | isDigitsOnly(guessSolutionString) == false)
                    {

                            Console.WriteLine("Please enter 4 digits.");
                            guessSolutionString = Console.ReadLine();

                    }


                    if (Convert.ToInt32(guessSolutionString) == Solution)
                    {
                        break;
                    }

                    checkAnswer(guessSolutionString, Solution);
                    turns++;
                }
                Console.WriteLine("Good job! You guessed my number with "+ turns + " guesses.");
                if (turns <4)
                {
                    Console.WriteLine("That's very impressive!");
                }
                else if (turns>8)
                {
                    Console.WriteLine("Hint for the next game - start thinking...");
                }
                Console.WriteLine("Press any key to restart, press Esc to close.");

                var finalChoice = Console.ReadKey();
                Console.Clear();
                if (finalChoice.Key == ConsoleKey.Escape)
                    Environment.Exit(0);
            }
        }
    }
}
