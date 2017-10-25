using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACEJsonExplorer
{
    class Program
    {
        public static string AuthToken { get; set; } = null;
        public static LauncherConfig Config = LauncherConfig.Load();

        static void Main(string[] args)
        {
            // Iterate through 5 test accounts ( 5 permission levels, each has a different permission)
            if (Config.TestAccounts?.Count > 0)
                foreach (var account in Config.TestAccounts)
                {
                    try { AuthToken = Authenticate.GetApiToken(account); }
                    catch (Exception error)
                    {
                        Console.WriteLine(error.Message);
                    }

                    if (AuthToken?.Length > 0)
                    {
                        Console.WriteLine(Operate.RequestWorldDeployment());
                    }
                    else
                        Console.Write("Error collecting auth token");
                    Console.ReadLine();
                }
            else
                Console.Write("Error in test accounts.");
        }
    }
}
