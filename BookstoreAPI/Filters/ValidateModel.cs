﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BookstoreAPI.Filters
{
    public class ValidateModel : ActionFilterAttribute
    {


        public override void OnActionExecuting(HttpActionContext actionContext) //override this method and create the required custom response.
        {
            if (actionContext.ModelState.IsValid == false)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }  //can write to the KIDAP Logs.



    }
}