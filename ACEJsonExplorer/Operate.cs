using Newtonsoft.Json;
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
        public static string RequestWorldDeployment()
        {
            Console.WriteLine("Attempting to use sync...");
            LauncherConfig config = Program.Config;

            string authToken = Program.AuthToken;
            if (authToken?.Length > 0)
            {
                var apiRequest = new RestRequest("/Server/RedeployWorldDatabase", Method.GET);
                apiRequest.AddHeader("Authorization", "Bearer " + authToken);
                RestClient cmdClient = new RestClient(config.GameApi);
                var apiResponse = cmdClient.Execute(apiRequest);

                if (apiResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    // show the error
                    Console.WriteLine("Error in sync request:");
                    return apiResponse.Content;
                }
                return apiResponse.Content;
            }
            return string.Empty;
        }
        
    }
}