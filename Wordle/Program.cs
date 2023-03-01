using Wordle;

namespace Worldle
{
    public static class Program
    {
        public static List<string> guesses = new List<string>();
        public static string Word = "";
        /// <summary>
        /// Generates a random number.
        /// </summary>
        /// <param name="min">Inclusive</param>
        /// <param name="max">Inclusive</param>
        /// <returns>Random number between parameters</returns>
        public static int randInt(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max + 1);
        }

        public static string Print(string str = "")
        {
            Console.Write(str + "\n");
            return str;
        }
        public static string Input(string str = "")
        {
            Console.Write(str);
            string input = Console.ReadLine();
            return input;
        }

        public static void Main()
        {
            Word = Wordlist.Words[randInt(0, Wordlist.Words.Count - 1)];
            bool gotWord = false;
            Dictionary<int, char> output = new Dictionary<int, char>();
            Console.ForegroundColor = ConsoleColor.White;
            Print("All words are 5 letters.");
            while (!gotWord)
            {
                TryGuess:
                Console.ForegroundColor = ConsoleColor.White;
                Print("Try guess the word!");
                string guess = Input("Type your guess here: ");
                if (!Wordlist.Words.Contains(guess.ToLower()))
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Print("That is not a valid guess.");
                    goto TryGuess;
                }
                if (guess.ToLower() == "idk" || guess.ToLower().Contains("give up") || guess.ToLower() == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    gotWord = true;
                    Print("Gave up. The word was " + Word + ".");
                    Print("Exiting game...");
                    Thread.Sleep(2000);
                    Environment.Exit(0);
                }
                if (guess.Length > 5 || guess.Length < 5)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Print("That is not a valid guess.");
                    goto TryGuess;
                }
                output.Clear();
                for (int i = 0; i < 5; i++)
                {
                    output.Add(i, guess[i]);
                }

                guesses.Add(guess);
                Console.Clear();
                int counter = 1;
                int tryCounter = 0;
                foreach (string guess_ in guesses)
                {
                    tryCounter++;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Guess " + counter + ": ");
                    for (int char_ = 0; char_ < 5; char_++)
                    {
                        if (!Wordlist.Words.Contains(guess_.ToLower()))
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Print("That is not a valid guess.");
                            goto TryGuess;
                        }
                        if (guess_.ToLower() == "idk" || guess_.ToLower().Contains("give up") || guess_.ToLower() == "exit")
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            gotWord = true;
                            Print("Gave up. The word was " + Word + ".");
                            Print("Exiting game...");
                            Thread.Sleep(2000);
                            Environment.Exit(0);
                        }
                        else if (Word.ToLower() == guess_.ToLower())
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            gotWord = true;
                            Print("You got the word!");
                            Print("Exiting game...");
                            Thread.Sleep(2000);
                            Environment.Exit(0);
                        }
                        if (Word.ToLower()[char_] == guess_.ToLower()[char_])
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(guess_.ToLower()[char_]);
                        }
                        else if (Word.ToLower().Contains(guess_.ToLower()[char_]))
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write(guess_.ToLower()[char_]);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(guess_.ToLower()[char_]);
                        }
                        //var array = output.ToArray();
                        //var first = array[i];
                        ////Console.ForegroundColor = first;
                        //Console.Write(first);
                    }
                    Print();
                    counter++;
                    if (tryCounter > 4)
                    {
                        Print("\n\nEnd of the game. You took too many tries.\n\nThe word was "+Word+".\nMaybe next time you might be more lucky!");
                        return;
                    }
                }
            }
        }
    }
}