using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Collections.Generic;
using BookstoreAPI.Models;

namespace BookstoreAPI.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<BookstoreAPI.Models.Bookstorecontext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            //AutomaticMigrationDataLossAllowed = true;
            
        }

        protected override void Seed(BookstoreAPI.Models.Bookstorecontext context)
        {
            //Fill in books table
            context.Books.AddOrUpdate(

              x => x.Title, new Book() { Author = "Premchand", Title = "Gaban", Cost = 35, Genre = Genre.Drama },
                new Book() { Author = "Bacchan", Title = "Nirmala", Cost = 25, Genre = Genre.Drama },
                new Book() { Author = "Premchand", Title = "Madhushala", Cost = 25, Genre = Genre.Action },

                new Book() { Author = "Gulzar", Title = "Raavi Paar", Cost = 35, Genre = Genre.Comedy }

                );
            context.SaveChanges();


            //Fill in customer table

            context.Customers.AddOrUpdate(

               x => x.Name, new Customer() { Name = "Anand Maran", Phone = "+61481336431", Email = "anand.maran@hotmail.com" },

               new Customer() { Name = "Priya Anand", Phone = "+61409094754", Email = "priya.anand@gmail.com" }

                );
            context.SaveChanges();




            //Fill in Rental table

            context.Rentals.AddOrUpdate(

              x => x.BookId, new Rentals()
              {
                  BookId = context.Books.Where(x => x.Title == "Gaban").Select(x => x.Id).FirstOrDefault(),
                  RentedTo = context.Customers.Where(x => x.Name == "Anand Maran").Select(x => x.Id).FirstOrDefault(),
                  RentedDate = DateTime.Today.Date
              }


                );
            context.SaveChanges();


            //Fill in reviews table

            context.Reviews.AddOrUpdate(

                new Review()
                {
                    Comment = "Its OK",
                    Rating = Rating.Ok,
                    BookId = context.Books.Where(x => x.Title == "Gaban").Select(x => x.Id).FirstOrDefault(),
                    CustomerId = context.Customers.Where(x => x.Name == "Anand Maran").Select(x => x.Id).FirstOrDefault()

                }



                );
            context.SaveChanges();

        }
    }
}
