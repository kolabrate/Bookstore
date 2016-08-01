using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Cors;
using BookstoreAPI.Filters.ErrorHandler;

namespace BookstoreAPI.Controllers
{

    [EnableCors(origins: "http://gumpmail.com.au,https://gumpmail.com.au", headers: "*", methods: "*")]
    //class that handles all 404 exceptions
    public class ErrorController : ApiController
    {

        [HttpGet, HttpPost, HttpPut, HttpDelete, HttpHead, HttpOptions]
        public IHttpActionResult NotFound()
        {

            var log = new ErrorLogger();
            log.NotFound("/");
             return NotFound();
        }
        
    }
}