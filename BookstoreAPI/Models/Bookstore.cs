using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace BookstoreAPI.Models
{

    #region BookStoreModel -  Books,Rental,Customer,Reviews

    #region book
    [Table("Books")]
    public class Book
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }


        [Required]
        public string Author { get; set; }


        [Required]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Cost { get; set; }


        public Genre Genre{ get; set; }


        //define all navigation properties

        public ICollection<Rentals> Rentals { get; set; }
        public ICollection<Review> Reviews { get; set; }



    }
    public enum Genre
    {
        Comedy,
        Drama,
        Action,
        Documentary,
        ScienceFiction

    };
    #endregion

    #region customer
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Phone]
        [Required]
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        
        public Location? Location { get; set; }


        //define all navigation properties
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Rentals> Rentals { get; set; }

    }

    public enum Location
    {

        Melbourne,
        Sydney,
        Adelaide,
        Brisbane,
        Perth

    }

    #endregion

    #region rental
    [Table("Rentals")]
    public class Rentals
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime RentedDate { get; set; }


        //define navigation properties & foreign keys
        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }

        [ForeignKey("Customer")]
        public int RentedTo { get; set; }
        public Customer Customer { get; set; }

    }
    #endregion

    #region review
    [Table("Reviews")]
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Comment { get; set; }
        public Rating? Rating { get; set; }



        //define navigation properties

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }


        [ForeignKey("Book")]
        public int BookId { get; set; }
        public Book Book { get; set; }

    }

    public enum Rating
    {
        Worse,
        Bad,
        Ok,
        Good,
        Brilliant

    }
    #endregion

    #endregion


    #region BookStore DB Context
    public class Bookstorecontext : DbContext
    {

        //invoke the base class constructor
        public Bookstorecontext() : base("name=BookStore") //connection string is defined and sent to base class constructor
        {

            //Fluent API can be called here if required.

            // Also a place to initialise the databaseinitialisers to see data
           // Database.Initialize(true);
            Database.SetInitializer(new Bookstoreinitialzer()); //(OR)


            //Database.SetInitializer(
            //    new MigrateDatabaseToLatestVersion<Bookstorecontext, Migrations.Configuration>(
            //        "BookStore"));

        }

        //Create tables in database
        public DbSet<Book> Books { get; set; }
        public DbSet<Rentals> Rentals { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Review> Reviews { get; set; }


    }


    public class Bookstoreinitialzer :DropCreateDatabaseIfModelChanges<Bookstorecontext>
    {


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
    #endregion


}