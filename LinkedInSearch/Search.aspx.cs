using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace LinkedInSearch
{
    public partial class Search : System.Web.UI.Page
    {

        private string AppId = ConfigurationManager.AppSettings["Client_ID"];
        private string password = ConfigurationManager.AppSettings["Client_Secret"];
        protected void Page_Load(object sender, EventArgs e)
        {
            TxtAuthCode.Text = Request.QueryString["code"].ToString();

        }

        protected void GetAccessToken_Click(object sender, EventArgs e)
        {

            try
            {
               
                string authorityUrl = "https://www.linkedin.com/oauth/v2/accessToken";
                string postParameters =
                    $"client_id={AppId}&client_secret={password}&code={TxtAuthCode.Text}&grant_type={"authorization_code"}&redirect_uri={"http://localhost:1724/search.aspx"}";
                ASCIIEncoding encodePostParam = new ASCIIEncoding();
                byte[] postParamByte = encodePostParam.GetBytes(postParameters);


                //Form the post request       
                HttpWebRequest getoAuthToken = WebRequest.Create(authorityUrl) as HttpWebRequest;
                getoAuthToken.Method = "POST";
                getoAuthToken.ContentType = "application/x-www-form-urlencoded";
                getoAuthToken.ContentLength = postParamByte.Length;
                Stream stream = getoAuthToken.GetRequestStream();
                stream.Write(postParamByte, 0, postParamByte.Length);
                stream.Close();


                HttpWebResponse oAuthResponse = getoAuthToken.GetResponse() as HttpWebResponse;

                //Read the response stream and retrieve the access token
                if (oAuthResponse != null)
                {
                    Stream oAuthResponseStream = oAuthResponse.GetResponseStream();
                    StreamReader oAuthResponseStreamReader = new StreamReader(oAuthResponseStream);

                    //Assign the access token
                    string accessToken = oAuthResponseStreamReader.ReadToEnd();
                    ReadAccessTokenFromStream(accessToken);

                }

                

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        //Read and write the accesstoken & refresh token
        protected void ReadAccessTokenFromStream(string accesstoken)
        {

            try
            {

                AcessTokenType accessTokenJson = JsonConvert.DeserializeObject<AcessTokenType>(accesstoken);
                //display access token and assign
                TxtAccessToken.Text = accessTokenJson.access_token.ToString();
                AccessSearch(TxtAccessToken.Text);



            }
            catch (Exception ex)
            {

                throw (ex);

            }




        }


        /// <summary>
        /// Search API invoked from here
        /// </summary>
        /// <param name="accessToken"></param>

        protected async void AccessSearch(string accessToken)
        {




            //string _requesturl = "https://api.linkedin.com/v1/people-search";
            string _requesturl = "https://www.linkedin.com/vsearch/p?keywords=sharepoint";
            HttpWebRequest oAuthCodeRequest = (HttpWebRequest)WebRequest.Create(_requesturl);
            oAuthCodeRequest.Method = "GET";
            oAuthCodeRequest.Headers["Authorization"] = "Bearer " + accessToken;
            oAuthCodeRequest.Accept = "*/*";

            var response = await oAuthCodeRequest.GetResponseAsync()
                                           .ConfigureAwait(continueOnCapturedContext: true)
                               as HttpWebResponse;



        }

    }

    //create an entity for Access Token to read
    public class AcessTokenType
    {

        public string token_type { get; set; }
        public string scope { get; set; }
        public string expires_in { get; set; }
        public string ext_expires_in { get; set; }
        public string expires_on { get; set; }
        public string not_before { get; set; }
        public string resource { get; set; }
        public string pwd_exp { get; set; }
        public string pwd_url { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string id_token { get; set; }

    }
}