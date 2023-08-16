using InlUpp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlUpp1.Helpers
{
    public class InputHandler
    {
        public static string GetUserAnswer(string question, string errorMessage)
        {
            while (true)
            {
                Console.WriteLine(question);
                string userAnswer = Console.ReadLine()!;

                if (!string.IsNullOrEmpty(userAnswer))
                {
                    return userAnswer;
                }
                else
                {
                    Console.WriteLine(errorMessage);
                }
            }
        }

        public static bool GetUserChoice()
        {

            Console.WriteLine("\n Do you want to save this advertisement ?");
            Console.WriteLine("\n");
            Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
            Console.WriteLine("\n (Y) - Yes");
            Console.WriteLine("\n (N) - No, start over");
            Console.WriteLine("\n *~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");

            ConsoleKey saveOrStartOver = Console.ReadKey().Key;
            Console.WriteLine();

            return saveOrStartOver == ConsoleKey.Y;
        }

        public static bool GetUserChoiceDelete()
        {

            Console.WriteLine("\n Do you want to delete this advertisement ?");
            Console.WriteLine("\n");
            Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
            Console.WriteLine("\n (Y) - Yes");
            Console.WriteLine("\n (N) - No");
            Console.WriteLine("\n *~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");

            ConsoleKey saveOrStartOver = Console.ReadKey().Key;
            Console.WriteLine();

            return saveOrStartOver == ConsoleKey.Y;
        }


    }
}
