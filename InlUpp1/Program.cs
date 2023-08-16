using ConsoleBlocket.DataContext;
using InlUpp1.Helpers;
using InlUpp1.Models;
using System;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static void Main(string[] args)
    {
        using (var database = new AppDatabase())
        {
            var app = new InlUpp1.Helpers.Application(new AdvertisementManager(database, new CategoryManager(database)));
            app.Run();
        }
    }
}





