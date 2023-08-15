// SAVING ANNONS APP
// ANNONS STRUCTURE - TITLE, DESCRIPTION, PRICE, CATEGORY
// YOU SHOULD BE ABLE TO CHOOSE CATEGORY
// CATEGORIES - Furniture, Mobil, Bicycle, Computers etc
// Create Read Update Delete Actions with DATABASE
// Search functionality based on Title and Category - should return list


using ConsoleBlocket.DataContext;
using InlUpp1.Helpers;
using InlUpp1.Models;
using System;


class Program
{
    static void Main(string[] args)
    {
        using (var context = new AppDatabase())
        {
            bool applicationRunning = true;

            List<Advertisment> advertisements = new List<Advertisment>();

            while (applicationRunning)
            {
                Console.WriteLine("Welcome to Blocket 2.0");
                Console.WriteLine("Available Options:");
                Console.WriteLine("(1) - Add new Advertisment");
                Console.WriteLine("(2) - Edit existing Advertisment");
                Console.WriteLine("(3) - Delete Advertisment");
                Console.WriteLine("(4) - Search Advertisment according to Title");
                Console.WriteLine("(5) - Search Advertisment according to Category");
                Console.WriteLine("Enter the command number: ");

                ConsoleKey userEntryValue = Console.ReadKey().Key;

                switch (userEntryValue)
                {
                    case ConsoleKey.D1:

                        bool createAdvertismentInProgress = true;

                        while (createAdvertismentInProgress)
                        {
                            string title = InputHandler.GetUserAnswer("\n What is the title for your advertisement", "Advertisement Title can not be empty");
                            string description = InputHandler.GetUserAnswer("\n What is the description for your advertisement", "Advertisement Description can not be empty");
                            string price = InputHandler.GetUserAnswer("\n What is the price for your advertisement", "Advertisement Price can not be empty");

                            Advertisment newAdvertisement = new Advertisment
                            {
                                Title = title,
                                Description = description,
                                Price = price
                            };

                            DisplayAdvertisement(newAdvertisement);

                            if (InputHandler.GetUserChoice())
                            {
                                Console.WriteLine("\n Save to database");

                                context.Advertisments.Add(newAdvertisement);
                                context.SaveChanges();

                                createAdvertismentInProgress = false;
                            }
                            else
                            {
                                Console.WriteLine("\n Start over");
                            }
                        }


                        break;
                    default:
                        Console.WriteLine(" \n Not a valid option");
                        break;
                }
            }
        }
    }
    static void DisplayAdvertisement(Advertisment advertisement)
    {
        Console.WriteLine("\n This is your advertisement");
        Console.WriteLine($"\n Title: {advertisement.Title}");
        Console.WriteLine($"\n Description: {advertisement.Description}");
        Console.WriteLine($"\n Price : {advertisement.Price}");
    }
}



