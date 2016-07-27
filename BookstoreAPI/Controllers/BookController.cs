using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BookstoreAPI.Models;
using System.Web.Http.Cors;

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
        public IEnumerable<Book> GetAllBooks()
        {
    
            return ctx.Books.ToList();

        }


        /// <summary>
        /// Returns a specific book based on a book ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("{id:int}")]
        [ResponseType(typeof(Book))]
        public IHttpActionResult GetBookbyIdName( int Id)
        {
            var book = ctx.Books.Find(Id);

            if(book == null)
            {
                return NotFound();

            }

           return Ok(book);

        }

        /// <summary>
        /// Add a new book to bookstore
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [Route("")]
        public HttpResponseMessage AddBook([FromBody] Book book)
        {

            if (ModelState.IsValid)
            {
                ctx.Books.Add(book);
                
            }
            else
            {
                Request.CreateResponse(HttpStatusCode.InternalServerError, "The model is not in the correct format");

            }
            return new HttpResponseMessage(HttpStatusCode.OK);

        }

        
    }
}