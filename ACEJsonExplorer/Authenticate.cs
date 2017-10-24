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
            RestClient authClient = new RestClient(Program.Config.LoginServer);
            Console.WriteLine($"Attempting to login with {Program.Config.Username} and {Program.Config.Password}.");

            var authRequest = new RestRequest("/Account/Authenticate", Method.POST);
            authRequest.AddJsonBody(new
            {
                Username = Program.Config.Username,
                Password = Program.Config.Password
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

            Console.WriteLine("Auth successful, grabbing token...");
            JObject response = JObject.Parse(authResponse.Content);
            authToken = (string)response.SelectToken("authToken");
            Console.WriteLine(authToken);
            if (authToken?.Length>0) { return authToken; }

            return string.Empty;
        }
    }
}