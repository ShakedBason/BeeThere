using System;
namespace BeThere.Services
{
    public class BaseService
    {
        private static readonly string s_BaseUrl = "http://localhost:5209/";
        //private static readonly string s_BaseUrl = "https://betherserverapi.azurewebsites.net";
        private static HttpClient s_ServerClient;


        public static HttpClient GetClient()
        {
            if (s_ServerClient is not null)
            {
                return s_ServerClient;
            }

            s_ServerClient = new HttpClient { BaseAddress = new Uri(s_BaseUrl) };
            return s_ServerClient;
        }
    }
}
