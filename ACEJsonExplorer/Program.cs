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
            try { AuthToken = Authenticate.GetApiToken();  }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }

            if (AuthToken?.Length > 0)
            {
                Console.WriteLine(Operate.Sync());
                Console.ReadLine();
            }
            else
                Console.Write("Error collecting auth token");
        }
    }
}
