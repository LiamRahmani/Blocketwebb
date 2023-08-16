using ConsoleBlocket.DataContext;
using InlUpp1.Helpers;
using InlUpp1.Models;
using System;


class Program
{
    static void Main(string[] args)
    {
        using (var database = new AppDatabase())
        {
            bool applicationRunning = true;

            List<Advertisment> advertisements = new List<Advertisment>();

            while (applicationRunning)
            {
                Console.Clear();
                Console.CursorVisible = false;
                DisplayAdvertisementOptions();

                ConsoleKey usersOptionChoice = Console.ReadKey().Key;

                switch (usersOptionChoice)
                {
                    case ConsoleKey.D1:

                        CreateAdvertisment(database);

                        break;

                    case ConsoleKey.D2:
                        Console.Clear();
                        ShowAllAdvertisments(database);
                        break;

                    case ConsoleKey.D3:
                        Console.Clear();
                        EditAdvertisment(database);
                        break;

                    case ConsoleKey.D4:
                        Console.Clear();
                        DeleteAdvertisment(database);
                        break;

                    case ConsoleKey.D5:
                        Console.Clear();
                        SearchAdvertismentsAccordingToTitle(database);
                        break;

                    default:
                        Console.WriteLine(" \n Not a valid option");
                        break;
                }
            }
        }
    }

   

    private static void ShowAllAdvertisments(AppDatabase database)
    {
        List<Advertisment> allAdvertisments = database.Advertisments.ToList();
        List<Category> allCategories = database.Categories.ToList();

        bool showingAllAdvertismentsProgress = true;
        while(showingAllAdvertismentsProgress)
        {
            Console.Clear();
            Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
            Console.WriteLine("ALL ADVERTISMENTS");
            Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
            foreach (Advertisment advertisment in allAdvertisments)
            {
                Category advertismentCategory = allCategories.Where(category => category.Id == advertisment.CategoryId).First();
                string categoryName = advertismentCategory.Name;
                Console.WriteLine("\n*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
                Console.WriteLine("Advertisment Title: {0}", advertisment.Title);
                Console.WriteLine("Description: {0}", advertisment.Description);
                Console.WriteLine("Price: {0}", advertisment.Price);
                Console.WriteLine("Category: {0}", categoryName);
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

    private static void CreateAdvertisment(AppDatabase database)
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
            Category category = DisplayAndChooseCategory(database);
            Console.Clear();



            Advertisment newAdvertisement = new Advertisment
            {
                Title = title,
                Description = description,
                Price = price,
                CategoryId = category.Id
            };

            DisplayCreatedAdvertisement(newAdvertisement, category.Name);

            if (InputHandler.GetUserChoice())
            {
                Console.WriteLine("\n Saved to database");

                database.Advertisments.Add(newAdvertisement);
                database.SaveChanges();

                createAdvertisementInProgress = false;
            }
            else
            {
                Console.WriteLine("\n Start over");
                Console.Clear();
            }
        }
    }

    private static void DisplayCreatedAdvertisement(Advertisment advertisement, string categoryName)
    {
        Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
        Console.WriteLine("\n This is your advertisement");
        Console.WriteLine($"\n Title: {advertisement.Title}");
        Console.WriteLine($"\n Description: {advertisement.Description}");
        Console.WriteLine($"\n Price : {advertisement.Price}");
        Console.WriteLine($"\n Category : {categoryName}");
        Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
    }

    private static Category DisplayAndChooseCategory(AppDatabase database)
    {
        List<Category> allCategories = database.Categories.ToList();
        int categoryListIndex = 0;

        Category selectedCategory = new Category();

        bool showingCategoriesProgress = true;
        while (showingCategoriesProgress)
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("Hello and welcome! Please choose category for your advertisment:");
            for (int i = 0; i < allCategories.Count(); i++)
            {
                Console.WriteLine((i == categoryListIndex ? "* " : "") + allCategories[i].Name + (i == categoryListIndex ? "<--" : ""));
            }
            var keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow && categoryListIndex != allCategories.Count() - 1)
            {
                categoryListIndex++;
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow && categoryListIndex >= 1)
            {
                categoryListIndex--;
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                selectedCategory.Name = allCategories[categoryListIndex].Name;
                selectedCategory.Id = allCategories[categoryListIndex].Id;
                showingCategoriesProgress = false;
            }
        }
        return selectedCategory;

    }

    private static void EditAdvertisment(AppDatabase database)
    {
        List<Advertisment> allAdvertisments = database.Advertisments.ToList();
        int advertismentIndex = 0;

        bool editingAdvertismentInProgress = true;
        while (editingAdvertismentInProgress)
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("Please choose the advertisment you want to edit:");
            for (int i = 0; i < allAdvertisments.Count(); i++)
            {
                Console.WriteLine((i == advertismentIndex ? "* " : "") + allAdvertisments[i].Title + (i == advertismentIndex ? "<--" : ""));
            }
            var keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow && advertismentIndex != allAdvertisments.Count() - 1)
            {
                advertismentIndex++;
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow && advertismentIndex >= 1)
            {
                advertismentIndex--;
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                Advertisment advertismentToEdit = allAdvertisments[advertismentIndex];
                Console.WriteLine($"You have choosen {advertismentToEdit.Title} to edit");

                Console.Clear();

                string title = InputHandler.GetUserAnswer("\n What is the updated title for your advertisement", "Advertisement Title cannot be empty");
                Console.Clear();
                string description = InputHandler.GetUserAnswer("\n What is the updated description for your advertisement", "Advertisement Description cannot be empty");
                Console.Clear();
                string price = InputHandler.GetUserAnswer("\n What is the updated price for your advertisement", "Advertisement Price cannot be empty");
                Console.Clear();
                Category category = DisplayAndChooseCategory(database);
                Console.Clear();



                advertismentToEdit.Title = title;
                advertismentToEdit.Description = description;
                advertismentToEdit.Price = price;
                advertismentToEdit.CategoryId = category.Id;

                DisplayCreatedAdvertisement(advertismentToEdit, category.Name);

                if (InputHandler.GetUserChoice())
                {
                    Console.WriteLine("\n Saved to database");

                    database.SaveChanges();

                    editingAdvertismentInProgress = false;
                }
                else
                {
                    Console.WriteLine("\n Start over");
                    Console.Clear();
                }
                
            }
        }

    }

    private static void DeleteAdvertisment(AppDatabase database)
    {
        List<Advertisment> allAdvertisments = database.Advertisments.ToList();
        int advertismentIndex = 0;

        bool deletingAdvertismentInProgress = true;
        while (deletingAdvertismentInProgress)
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("Please choose the advertisment you want to delete:");
            for (int i = 0; i < allAdvertisments.Count(); i++)
            {
                Console.WriteLine((i == advertismentIndex ? "* " : "") + allAdvertisments[i].Title + (i == advertismentIndex ? "<--" : ""));
            }
            var keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow && advertismentIndex != allAdvertisments.Count() - 1)
            {
                advertismentIndex++;
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow && advertismentIndex >= 1)
            {
                advertismentIndex--;
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                Advertisment advertismentToDelete = allAdvertisments[advertismentIndex];
                Console.WriteLine($"You have choosen {advertismentToDelete.Title} to delete");

                Console.Clear();

                if (InputHandler.GetUserChoiceDelete())
                {
                    
                    database.Advertisments.Remove( advertismentToDelete );
                    database.SaveChanges();

                    Console.WriteLine("\n Deleted from database");

                    deletingAdvertismentInProgress = false;
                }
                else
                {
                    Console.WriteLine("\n Start over");
                    Console.Clear();
                }

            }
        }
    }

    private static string ReturnNameOfCategoryBasedOnAdvertismentCategoryId (int categoryId, AppDatabase database)
    {
        List<Category> allCategories = database.Categories.ToList();
        Category categoryInQuestion = allCategories.Where(category => category.Id == categoryId).First();
        return categoryInQuestion.Name;
    }

    private static void SearchAdvertismentsAccordingToTitle(AppDatabase database)
    {
        List<Advertisment> allAdvertisments = database.Advertisments.ToList();

        bool searchAdvertismentInProgress = true;

        while (searchAdvertismentInProgress)
        {
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine("Enter the search keyword");
            string userKeyword = Console.ReadLine()!;
            List<Advertisment> searchedAdvertisments = allAdvertisments.Where(advertisment => advertisment.Title.Contains(userKeyword, StringComparison.OrdinalIgnoreCase)).ToList();
            int advertismentIndex = 0;
            for (int i = 0; i < allAdvertisments.Count(); i++)
            {
                Console.WriteLine((i == advertismentIndex ? "* " : "") + allAdvertisments[i].Title + (i == advertismentIndex ? "<--" : ""));
            }
            var keyPressed = Console.ReadKey();

            if (keyPressed.Key == ConsoleKey.DownArrow && advertismentIndex != allAdvertisments.Count() - 1)
            {
                advertismentIndex++;
            }
            else if (keyPressed.Key == ConsoleKey.UpArrow && advertismentIndex >= 1)
            {
                advertismentIndex--;
            }
            else if (keyPressed.Key == ConsoleKey.Enter)
            {
                Advertisment advertismentSelected = allAdvertisments[advertismentIndex];

                string categoryName = ReturnNameOfCategoryBasedOnAdvertismentCategoryId(advertismentSelected.CategoryId, database);

                Console.Clear();
                DisplayCreatedAdvertisement(advertismentSelected, categoryName);
                Console.WriteLine("\n Press any button to go back to main menu");

                Console.ReadLine();
                searchAdvertismentInProgress = false;


            }
        }

    }
}



