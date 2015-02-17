using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTabletop.SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string baseAddress = "http://localhost:63096/";
            StartOptions startOptions = new StartOptions();
            startOptions.Urls.Add(baseAddress);

            using (WebApp.Start<Startup>(startOptions))
            {
                // Create HttpCient and make a request to api/values 
                //HttpClient client = new HttpClient();
                //var response = client.GetAsync(baseAddress + "player/info/55").Result;

                while (true)
                {
                    Task.Delay(TimeSpan.FromSeconds(1)).Wait();
                }

            }

            
            Console.ReadLine();
        }
    }
}
