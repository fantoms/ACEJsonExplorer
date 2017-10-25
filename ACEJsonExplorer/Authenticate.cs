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
        public static string GetApiToken(Account account)
        {
            RestClient authClient = new RestClient(Program.Config.LoginServer);
            Console.WriteLine($"Attempting to login with {account.Username} and {account.Password}.");

            var authRequest = new RestRequest("/Account/Authenticate", Method.POST);
            authRequest.AddJsonBody(new
            {
                Username = account.Username,
                Password = account.Password
            });
            var authResponse = authClient.Execute(authRequest);
            string authToken;

            if (authResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                // show the error
                Console.WriteLine("Error logging in: ");
                if (authResponse.ErrorMessage?.Length > 0)
                    Console.WriteLine(authResponse.ErrorMessage);
                return string.Empty;
            } else
                Console.WriteLine(authResponse.Content);

            Console.WriteLine("Auth successful, grabbing token...");
            JObject response = JObject.Parse(authResponse.Content);
            authToken = (string)response.SelectToken("authToken");
            Console.WriteLine(authToken);
            if (authToken?.Length>0) { return authToken; }

            return string.Empty;
        }
    }
}