using ConsoleBlocket.DataContext;
using InlUpp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlUpp1.Helpers
{
    class AdvertisementManager
    {
        private readonly AppDatabase _database;
        private readonly CategoryManager _categoryManager;

        public AdvertisementManager(AppDatabase database, CategoryManager categoryManager)
        {
            _database = database;
            _categoryManager = categoryManager;
        }

        public void CreateAdvertisement()
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
                Category category = _categoryManager.ChooseCategory();
                Console.Clear();



                Advertisment newAdvertisement = new Advertisment
                {
                    Title = title,
                    Description = description,
                    Price = price,
                    CategoryId = category.Id
                };

                DisplayAdvertisment(newAdvertisement, category.Name);

                if (InputHandler.GetUserChoice())
                {
                    Console.WriteLine("\n Saved to database");

                    _database.Advertisments.Add(newAdvertisement);
                    _database.SaveChanges();

                    createAdvertisementInProgress = false;
                }
                else
                {
                    Console.WriteLine("\n Start over");
                    Console.Clear();
                }
            }
        }

        private void DisplayAdvertisment(Advertisment newAdvertisement, string categoryName)
        {
            Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
            Console.WriteLine("\n Advertisement");
            Console.WriteLine($"\n Title: {newAdvertisement.Title}");
            Console.WriteLine($"\n Description: {newAdvertisement.Description}");
            Console.WriteLine($"\n Price : {newAdvertisement.Price}");
            Console.WriteLine($"\n Category : {categoryName}");
            Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
        }

        public void ShowAllAdvertisements()
        {
            List<Advertisment> allAdvertisments = _database.Advertisments.ToList();
            List<Category> allCategories = _categoryManager.GetAllCategories();

            bool showingAllAdvertismentsProgress = true;
            while (showingAllAdvertismentsProgress)
            {
                Console.Clear();
                Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
                Console.WriteLine("ALL ADVERTISMENTS");
                Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");

                if (!allAdvertisments.Any())
                {
                    Console.Clear();
                    Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
                    Console.WriteLine("Currently there are not advertisments in the database");
                    Console.WriteLine("*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*");
                }

                foreach (Advertisment advertisment in allAdvertisments)
                {
                    Category advertismentCategory = allCategories.Where(category => category.Id == advertisment.CategoryId).First();
                    string categoryName = advertismentCategory.Name;
                    DisplayAdvertisment(advertisment, categoryName);
                }

                Console.WriteLine("\n Press any button to go back to main menu");
                Console.ReadKey();
                showingAllAdvertismentsProgress = false;
            }
        }

        public void EditAdvertisement()
        {
            List<Advertisment> allAdvertisments = _database.Advertisments.ToList();
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

                    string updatedTitle = InputHandler.GetUserAnswer("\n What is the updated title for your advertisement", "Advertisement Title cannot be empty");
                    Console.Clear();
                    string updatedDescription = InputHandler.GetUserAnswer("\n What is the updated description for your advertisement", "Advertisement Description cannot be empty");
                    Console.Clear();
                    string updatedPrice = InputHandler.GetUserAnswer("\n What is the updated price for your advertisement", "Advertisement Price cannot be empty");
                    Console.Clear();
                    Category updatedCategory = _categoryManager.ChooseCategory();
                    Console.Clear();

                    advertismentToEdit.Title = updatedTitle;
                    advertismentToEdit.Description = updatedDescription;
                    advertismentToEdit.Price = updatedPrice;
                    advertismentToEdit.CategoryId = updatedCategory.Id;

                    DisplayAdvertisment(advertismentToEdit, updatedCategory.Name);

                    if (InputHandler.GetUserChoice())
                    {
                        Console.WriteLine("\n Saved to database");

                        _database.SaveChanges();

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

        public void DeleteAdvertisement()
        {
            List<Advertisment> allAdvertisments = _database.Advertisments.ToList();
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
                        _database.Advertisments.Remove(advertismentToDelete);
                        _database.SaveChanges();

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

        public void SearchAdvertisementsByTitle()
        {
            List<Advertisment> allAdvertisments = _database.Advertisments.ToList();

            bool searchAdvertismentInProgress = true;

            while (searchAdvertismentInProgress)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.WriteLine("Enter the title search keyword");
                string userKeyword = Console.ReadLine()!;
                List<Advertisment> searchedAdvertisments = allAdvertisments.Where(advertisment => advertisment.Title.Contains(userKeyword, StringComparison.OrdinalIgnoreCase)).ToList();
                int advertismentIndex = 0;

                if (!searchedAdvertisments.Any())
                {
                    Console.Clear();
                    Console.WriteLine("No advertisements found matching the search criteria.");
                    Console.WriteLine("\n Press any button to go back to main menu");
                    Console.ReadLine();
                    searchAdvertismentInProgress = false;
                }
                else
                {
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

                        string categoryName = _categoryManager.GetCategoryByID(advertismentSelected.CategoryId).Name;

                        Console.Clear();
                        DisplayAdvertisment(advertismentSelected, categoryName);
                        Console.WriteLine("\n Press any button to go back to main menu");

                        Console.ReadLine();
                        searchAdvertismentInProgress = false;
                    }
                }
            }
        }

        public void SearchAdvertisementsByCategory()
        {
            List<Advertisment> allAdvertisments = _database.Advertisments.ToList();

            List<Category> allCategories = _database.Categories.ToList();

            bool searchAdvertismentInProgress = true;

            while (searchAdvertismentInProgress)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Category categoryChosen = _categoryManager.ChooseCategory();
                Console.Clear();
                List<Category> categoriesSearched = allCategories.Where(category => category.Name.Contains(categoryChosen.Name, StringComparison.OrdinalIgnoreCase)).ToList();
                List<int> categoryIds = categoriesSearched.Select(category => category.Id).ToList();

                List<Advertisment> advertismentSearched = allAdvertisments.Where(advertisement => categoryIds.Contains(advertisement.CategoryId)).ToList();
                if (!advertismentSearched.Any())
                {
                    Console.WriteLine("No advertisements found matching the search criteria.");
                    Console.WriteLine("\n Press any button to go back to main menu");
                    Console.ReadLine();
                    searchAdvertismentInProgress = false;
                }
                else
                {
                    int advertismentIndex = 0;
                    for (int i = 0; i < advertismentSearched.Count(); i++)
                    {
                        Console.WriteLine((i == advertismentIndex ? "* " : "") + advertismentSearched[i].Title + (i == advertismentIndex ? "<--" : ""));
                    }
                    var keyPressed = Console.ReadKey();

                    if (keyPressed.Key == ConsoleKey.DownArrow && advertismentIndex != advertismentSearched.Count() - 1)
                    {
                        advertismentIndex++;
                    }
                    else if (keyPressed.Key == ConsoleKey.UpArrow && advertismentIndex >= 1)
                    {
                        advertismentIndex--;
                    }
                    else if (keyPressed.Key == ConsoleKey.Enter)
                    {
                        Advertisment advertismentSelected = advertismentSearched[advertismentIndex];

                        string categoryName = _categoryManager.GetCategoryByID(advertismentSelected.CategoryId).Name;

                        Console.Clear();
                        DisplayAdvertisment(advertismentSelected, categoryName);
                        Console.WriteLine("\n Press any button to go back to main menu");
                        Console.ReadLine();
                        searchAdvertismentInProgress = false;
                    }
                }
            }
        }
    }
}
