using ConsoleBlocket.DataContext;
using InlUpp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InlUpp1.Helpers
{
    class CategoryManager
    {
        private readonly AppDatabase _database;

        public CategoryManager(AppDatabase database)
        {
            _database = database;
        }

        public List<Category> GetAllCategories()
        {
            List<Category> allCategories = _database.Categories.ToList();
            return allCategories;
        }

        public Category GetCategoryByID(int id) 
        {
            List<Category> allCategories = _database.Categories.ToList();
            Category categoryById = allCategories.Where(category => category.Id == id).FirstOrDefault()!;
            return categoryById;
        }

        public Category ChooseCategory()
        {
            List<Category> allCategoriesFromDatabase = GetAllCategories();
            
            int categoryListIndex = 0;

            Category selectedCategory = new Category();

            bool showingCategoriesProgress = true;
            while (showingCategoriesProgress)
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.WriteLine("Please choose category for your advertisment:");

                DisplayCategories(allCategoriesFromDatabase, categoryListIndex);

                var keyPressed = Console.ReadKey();

                if (keyPressed.Key == ConsoleKey.DownArrow && categoryListIndex != allCategoriesFromDatabase.Count() - 1)
                {
                    categoryListIndex++;
                }
                else if (keyPressed.Key == ConsoleKey.UpArrow && categoryListIndex >= 1)
                {
                    categoryListIndex--;
                }
                else if (keyPressed.Key == ConsoleKey.Enter)
                {
                    selectedCategory = allCategoriesFromDatabase[categoryListIndex];
                    showingCategoriesProgress = false;
                }
            }
            return selectedCategory;
        }

        private static void DisplayCategories(List<Category> allCategoriesFromDatabase, int categoryListIndex)
        {
            for (int i = 0; i < allCategoriesFromDatabase.Count(); i++)
            {
                Console.WriteLine((i == categoryListIndex ? "* " : "") + allCategoriesFromDatabase[i].Name + (i == categoryListIndex ? "<--" : ""));
            }
        }
    }
}
