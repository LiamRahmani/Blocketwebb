using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InlUpp1.Helpers;

namespace InlUpp1
{
    class Application
    {
        private readonly AdvertisementManager _advertisementManager;

        public Application(AdvertisementManager advertisementManager)
        {
            _advertisementManager = advertisementManager;
        }

        public void Run()
        {
            bool applicationRunning = true;

            while (applicationRunning)
            {
                Console.Clear();
                DisplayAdvertisementOptions();

                ConsoleKey usersOptionChoice = Console.ReadKey().Key;

                switch (usersOptionChoice)
                {
                    case ConsoleKey.D1:
                        _advertisementManager.CreateAdvertisement();
                        break;
                    case ConsoleKey.D2:
                        _advertisementManager.ShowAllAdvertisements();
                        break;
                    case ConsoleKey.D3:
                        _advertisementManager.EditAdvertisement();
                        break;
                    case ConsoleKey.D4:
                        _advertisementManager.DeleteAdvertisement();
                        break;
                    case ConsoleKey.D5:
                        _advertisementManager.SearchAdvertisementsByTitle();
                        break;
                    case ConsoleKey.D6:
                        _advertisementManager.SearchAdvertisementsByCategory();
                        break;
                    default:
                        Console.WriteLine(" \n Not a valid option");
                        break;
                }
            }
        }

        private void DisplayAdvertisementOptions()
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
    }
}
