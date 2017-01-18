using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarkovWordChainLib;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        /// <summary>
        /// Writes an example to the console of how one could use the MarkovWordChainLib library. The method is rather messy, but the console output should be clear.
        /// </summary>
        void Run()
        {
            string sentence = "I am not phone, I am a sentence. I can not make calls, but I can make sense.";
            string[] arr = sentence.Split(' ');
            MarkovGen gen = new MarkovGen(arr);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("=== The sentence ===");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(sentence);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n=== Here is a list of all words in the sentence and their child words ===");
            Console.ForegroundColor = ConsoleColor.Gray;
            gen.GetWords().ToList().ForEach(x =>
            {
                Console.WriteLine("> " + x.TheWord);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                x.GetChildWords().ToList().ForEach(y =>
                {
                    Console.WriteLine("  - " + y.TheWord);
                });
                Console.ForegroundColor = ConsoleColor.Gray;
            });

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n=== The list from above, with more words added to it dynamically ===");
            Console.ForegroundColor = ConsoleColor.Gray;
            gen.AddWord("Smartphone."); //Adds a single word.
            gen.AddWords("Added more words I know.".Split(' ')); //Adds multiple words.
            gen.GetWords().ToList().ForEach(x =>
            {
                Console.WriteLine("> " + x.TheWord);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                x.GetChildWords().ToList().ForEach(y =>
                {
                    Console.WriteLine("  - " + y.TheWord);
                });
                Console.ForegroundColor = ConsoleColor.Gray;
            });

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n=== Here is an example of recursive sentence construction ===");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("\"");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(GenerateRecursiveSentence(gen.GetRandomWord()));
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\"");
            Console.WriteLine("This sentence is not guaranteed to be randomized, due to the low amount of different words in the sentence.");

            Console.WriteLine("\nDemo complete. You might need to scroll up to see the entire demo.");
            Console.ReadLine();
        }

        /// <summary>
        /// Simple example of recursive sentence generation, using the MarkovWordChainLib library.
        /// </summary>
        /// <param name="maxLength"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        string GenerateRecursiveSentence(Word word)
        {
            if (!word.HasChildWords() || (word.GetChildWords().Length == 1 && word.GetChildWords()[0].TheWord == word.TheWord))
                return word.TheWord;
            return word.TheWord + " " + GenerateRecursiveSentence(word.GetRandomChildWord());
        }
    }
}