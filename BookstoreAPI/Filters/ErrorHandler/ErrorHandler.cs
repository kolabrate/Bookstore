using System.Web;
using System.Web.Http;
using System;


namespace BookstoreAPI.Filters.ErrorHandler
{

    //Log unhandled and handled exceptions to ELMAH data source
    public class ErrorLogger : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }


        //When an ID (Or) an object is not 
        public void NotFound(int Id)
        {
            // log error to ELMAH
            Elmah.ErrorSignal.FromCurrentContext().Raise(new HttpException(404, string.Format("{0} not found",Id)));

           
        }

        //When a route is incorrect
        public void NotFound(string Route)
        {
            // log error to ELMAH
            Elmah.ErrorSignal.FromCurrentContext().Raise(new HttpException(404, string.Format("{0} not found", Route)));

            
        }

        //500 errors can be captured in this method
        public void InternalError(Exception ex)
        {


            
        }



    }
}