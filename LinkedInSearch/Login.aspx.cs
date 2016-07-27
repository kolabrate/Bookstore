using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

namespace LinkedInSearch
{
    public partial class Login : System.Web.UI.Page
    {

        private string AppId = ConfigurationManager.AppSettings["Client_ID"];
        private string password = ConfigurationManager.AppSettings["Client_Secret"];

        protected void Page_Load(object sender, EventArgs e)
        {




        }

        protected void SignIn_Click(object sender, EventArgs e)
        {

            try
            {



                string request_url =
                String.Format(
                    "https://www.linkedin.com/oauth/v2/authorization?response_type=code&client_id={0}&redirect_uri={1}",
                    AppId, "http://localhost:1724/search.aspx");

                HttpWebRequest oAuthCodeRequest = (HttpWebRequest)WebRequest.Create(request_url);
                oAuthCodeRequest.Method = "GET";

                HttpWebResponse oAuthResponse = (HttpWebResponse)oAuthCodeRequest.GetResponse();
                HttpContext.Current.Response.Redirect(oAuthResponse.ResponseUri.ToString());


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message.ToString());





            }
        }
    }
}