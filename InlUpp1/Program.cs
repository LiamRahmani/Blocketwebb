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
                Console.Clear();
                DisplayAdvertisementOptions();

                ConsoleKey usersOptionChoice = Console.ReadKey().Key;

                switch (usersOptionChoice)
                {
                    case ConsoleKey.D1:

                        CreateAdvertisment(context);

                        break;

                    case ConsoleKey.D2:

                        ShowAllAdvertisments(context);
                        break;

                    default:
                        Console.WriteLine(" \n Not a valid option");
                        break;
                }
            }
        }
    }

    private static void ShowAllAdvertisments(AppDatabase context)
    {
        List<Advertisment> allAdvertisments = context.Advertisments.ToList();

        bool showingAllAdvertismentsProgress = true;
        while(showingAllAdvertismentsProgress)
        {
            Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
            Console.WriteLine("ALL ADVERTISMENTS");
            Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
            foreach (Advertisment advertisment in allAdvertisments)
            {
                Console.WriteLine("\n*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
                Console.WriteLine("Advertisment Title: {0}", advertisment.Title);
                Console.WriteLine("Description: {0}", advertisment.Description);
                Console.WriteLine("Price: {0}", advertisment.Price);
                Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
               
                
            }
            Console.WriteLine("\n Press any button to go back to main menu");
            ConsoleKey anyKey = Console.ReadKey().Key;
            switch (anyKey)
            {
                default:
                    showingAllAdvertismentsProgress = false;
                    break;
            }
        }
       
    }

    private static void DisplayCreatedAdvertisement(Advertisment advertisement)
    {
        Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
        Console.WriteLine("\n This is your advertisement");
        Console.WriteLine($"\n Title: {advertisement.Title}");
        Console.WriteLine($"\n Description: {advertisement.Description}");
        Console.WriteLine($"\n Price : {advertisement.Price}");
        Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
    }

    private static void DisplayAdvertisementOptions()
    {
        Console.WriteLine("Welcome to Blocket 2.0");
        Console.WriteLine("\n *~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
        Console.WriteLine("Available Options:");
        Console.WriteLine("(1) - Add new Advertisment");
        Console.WriteLine("(2) - Show all Advertisment");
        Console.WriteLine("(3) - Edit existing Advertisment");
        Console.WriteLine("(4) - Delete Advertisment");
        Console.WriteLine("(5) - Search Advertisment according to Title");
        Console.WriteLine("(6) - Search Advertisment according to Category");
        Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");

        Console.WriteLine("\n Enter the command number: ");
        Console.WriteLine();
    }

    private static void CreateAdvertisment(AppDatabase context)
    {
        Console.Clear();
        bool createAdvertisementInProgress = true;

        while (createAdvertisementInProgress)
        {
            string title = InputHandler.GetUserAnswer("\n What is the title for your advertisement", "Advertisement Title cannot be empty");
            Console.Clear();
            string description = InputHandler.GetUserAnswer("\n What is the description for your advertisement", "Advertisement Description cannot be empty");
            Console.Clear();
            string price = InputHandler.GetUserAnswer("\n What is the price for your advertisement", "Advertisement Price cannot be empty");
            Console.Clear();

            Advertisment newAdvertisement = new Advertisment
            {
                Title = title,
                Description = description,
                Price = price
            };

            DisplayCreatedAdvertisement(newAdvertisement);

            if (InputHandler.GetUserChoice())
            {
                Console.WriteLine("\n Saved to database");

                context.Advertisments.Add(newAdvertisement);
                context.SaveChanges();

                createAdvertisementInProgress = false;
            }
            else
            {
                Console.WriteLine("\n Start over");
                Console.Clear();
            }
        }
    }
}



