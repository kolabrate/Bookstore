using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookstoreAPI.Dto;
using BookstoreAPI.Models;
using BookstoreAPI.Filters;

namespace BookstoreAPI.Repos
{
    public class BookRepository : IDisposable, IBookRepository
    {
        private Bookstorecontext ctx = new Bookstorecontext();


        #region Book Repo Business & Data Access

        //Return all books - business logic
        public IEnumerable<Bookdto> ReturnBooks()
        {

            var _books = new List<Bookdto>();
            foreach (var _book in ctx.Books)
            {
                _books.Add(new Bookdto { Id = _book.Id, Author = _book.Author, Title = _book.Title, Cost = _book.Cost, Genre = _book.Genre.ToString() });

            }
            return _books;

        }

        //Return a book - on a specific ID
        public Book GetBookbyIdName(int? Id)
        {
            var book = ctx.Books.Find(Id);

            if (book == null)
            {
                return null;

            }
            else
            {
                return book;

            }


        }

        //Add a new book
        public void AddBook(Bookdto bookdto)
        {

            ctx.Books.Add(new Book { Author = bookdto.Author, Cost = bookdto.Cost, Genre = (Genre)Enum.Parse(typeof(Genre), bookdto.Genre), Title = bookdto.Title });
            ctx.SaveChanges();
            

        }


        //Update book info
        public void UpdateBook(Bookdto bookdto)
        {



        }



        //Delete's a book
        public void DeleteBook(int Id)
        {

            ctx.Books.Remove(ctx.Books.Find(Id));
            ctx.SaveChanges();
            

        }

        #endregion


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~BookRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion



    }


    public interface IBookRepository
    {

       IEnumerable<Bookdto> ReturnBooks();

       Book GetBookbyIdName(int? Id);

       void AddBook(Bookdto bookdto);

       void UpdateBook(Bookdto bookdto);

       void DeleteBook(int Id);

       
    }


}