using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACEJsonExplorer
{
    public static class Operate
    {
        public static string Sync()
        {
            Console.WriteLine("Attempting to use sync...");
            LauncherConfig config = Program.Config;
            RestClient authClient = new RestClient(config.LoginServer);

            string authToken = Program.AuthToken;
            if (authToken?.Length > 0)
            {
                var apiRequest = new RestRequest("/Sync/Pull", Method.POST);
                apiRequest.AddHeader("Authorization", "Bearer " + authToken);
                var apiResponse = authClient.Execute(apiRequest);
                
                if (apiResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    // show the error
                    Console.WriteLine("Error in sync request:");
                    Console.WriteLine(apiResponse.Content);
                    return apiResponse.Content;
                }
                return apiResponse.Content;
            }
            return string.Empty;
        }
    }
}