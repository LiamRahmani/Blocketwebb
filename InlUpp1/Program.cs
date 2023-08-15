// SAVING ANNONS APP
// ANNONS STRUCTURE - TITLE, DESCRIPTION, PRICE, CATEGORY
// YOU SHOULD BE ABLE TO CHOOSE CATEGORY
// CATEGORIES - Furniture, Mobil, Bicycle, Computers etc
// Create Read Update Delete Actions with DATABASE
// Search functionality based on Title and Category - should return list


using InlUpp1.Models;

bool applicationRunning = true;

while(applicationRunning)
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
            // PROCESS OF CREATING A ADVERTISMENT
            // advertisment is a object that has its properties so we create a class

            bool createAdvertismentInProgress = true;

            List<Advertisment> advertisements = new List<Advertisment>();

            Advertisment newAdvertisment = new Advertisment();

            bool titleIsSet = false;
            bool descriptionIsSet = false;
            bool priceIsSet = false;


            if (!string.IsNullOrEmpty(newAdvertisment.Title))
            {
                Console.WriteLine($"\n Advertisement Title: {newAdvertisment.Title}");
            }

            while (createAdvertismentInProgress)
            {
                if (!string.IsNullOrEmpty(newAdvertisment.Title))
                {
                    Console.WriteLine($"\n Advertisement Title: {newAdvertisment.Title}");
                } else
                {
                    Console.WriteLine(" \n What is the title for your advertisment");
                    newAdvertisment.Title = Console.ReadLine();
                    titleIsSet = true;
                }

                if (!string.IsNullOrEmpty(newAdvertisment.Description) && titleIsSet)
                {
                    Console.WriteLine($"\nAdvertisement Description: {newAdvertisment.Description}");
                }
                else
                {
                    Console.WriteLine(" \n What is the description for your advertisment");
                    newAdvertisment.Description = Console.ReadLine();
                    descriptionIsSet = true;
                }

                if (!string.IsNullOrEmpty(newAdvertisment.Price) && priceIsSet && descriptionIsSet)
                {
                    Console.WriteLine($"\n Advertisement Price: {newAdvertisment.Price}");
                }
                else
                {
                    Console.WriteLine(" \n What is the price for your advertisment");
                    newAdvertisment.Price = Console.ReadLine();
                    priceIsSet = true;
                }


                //createAdvertismentInProgress = false;
            }
            
            break;
        default:
            Console.WriteLine(" \n Not a valid option");
            break;
    }
}


