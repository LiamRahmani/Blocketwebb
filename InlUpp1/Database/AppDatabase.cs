using InlUpp1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace ConsoleBlocket.DataContext
{
    internal class AppDatabase : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>().HasData(

                new Category { Id = 1, Name = "Furniture"},

                new Category { Id = 2, Name = "Mobile phone"},

                new Category { Id = 3, Name = "Computers"},

                new Category { Id = 4, Name = "Clothes" },

                new Category { Id = 5, Name = "Cars" },

                new Category { Id = 6, Name = "Shoes" }

            );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string appSettingsPath = Path.Combine(basePath, "appsettings.json");

            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile(appSettingsPath, optional: false, reloadOnChange: true)
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection")!;

            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Advertisment> Advertisments => Set<Advertisment>();
        public DbSet<Category> Categories => Set<Category>();

        

    }
}
