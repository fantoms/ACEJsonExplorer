﻿using Newtonsoft.Json;
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

            if (Program.AuthToken?.Length > 0)
            {
                RestClient cmdClient = new RestClient(Program.Config.GameApi);
                var apiRequest = new RestRequest("/Server/RedeployWorldDatabase", Method.GET);
                apiRequest.AddHeader("Authorization", "Bearer " + Program.AuthToken);                
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