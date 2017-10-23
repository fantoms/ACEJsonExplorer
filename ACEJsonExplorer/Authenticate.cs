using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACEJsonExplorer
{
    public static class Authenticate
    {
        public static string GetApiToken()
        {
            LauncherConfig config = Program.Config;
            RestClient authClient = new RestClient(config.LoginServer);
            var authRequest = new RestRequest("/Account/Authenticate", Method.POST);
            authRequest.AddJsonBody(new
            {
                Username = config.Username,
                Password = config.Password
            });
            var authResponse = authClient.Execute(authRequest);
            string authToken;

            if (authResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                // show the error
                Console.WriteLine("Error logging in");
                Console.WriteLine(authResponse.Content);
                return string.Empty;
            }

            Console.WriteLine("Auth successful, retreiving subscriptions...");
            JObject response = JObject.Parse(authResponse.Content);
            authToken = (string)response.SelectToken("authToken");

            if (authToken?.Length>0) { return authToken; }

            return string.Empty;
        }
    }
}