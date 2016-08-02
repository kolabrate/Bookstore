using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;
using BookstoreAPI.Dto;
using BookstoreAPI.Models;
using BookstoreAPI.Filters;


namespace BookstoreAPI.Controllers
{

    [EnableCors(origins: "http://gumpmail.com.au,https://gumpmail.com.au", headers: "*", methods: "*")]
    [RoutePrefix("api/books")]
    public class BookController : ApiController
    {
        private Bookstorecontext ctx = new Bookstorecontext();


        /// <summary>
        /// Returns all books from database
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public IEnumerable<Bookdto> GetAllBooks()
        {

           var _books = new List<Bookdto>();
           foreach(var _book in ctx.Books)
            {
                _books.Add(new Bookdto { Id = _book.Id, Author = _book.Author, Title = _book.Title, Cost = _book.Cost, Genre = _book.Genre.ToString() });

            }
            return _books;

        }


        /// <summary>
        /// Returns a specific book based on a book ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBookbyIdName(int Id)
        {
            var book = ctx.Books.Find(Id);

            if (book == null)
            {
               // return (IHttpActionResult)new NotImplementedException(string.Format("The Book ID:{0} does not exist ", Id));
               return InternalServerError(new Exception(string.Format("Book ID : {0} not found", Id)));

                //var logger = new Filters.ErrorHandler.ErrorLogger();
                //logger.NotFound(Id);

            }

            return Ok(book);

        }


        

        /// <summary>
        /// Add a new book to bookstore
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        [ValidateModel]
        public HttpResponseMessage AddBook([FromBody]Bookdto bookdto)
        {
              
            ctx.Books.Add(new Book { Author = bookdto.Author, Cost = bookdto.Cost, Genre = (Genre)Enum.Parse(typeof(Genre), bookdto.Genre), Title = bookdto.Title });
            ctx.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Book added succ!") };
                

        }


        /// <summary>
        /// Delete's list of books
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage DeleteBook([FromBody]int Id)
        {
            
            ctx.Books.Remove(ctx.Books.Find(Id));
            ctx.SaveChanges();
            return new HttpResponseMessage(HttpStatusCode.OK);
            
        }

      

    }
}