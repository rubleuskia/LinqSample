using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AnalyticsProgram.Utils
{
    class WebSiteUtils
    {
        private const string UserAgent = "Mozilla/4.0(compatible;MSIE 6.0;Windows NT 5.2;.NET CLR 1.0.3705;)";

        public static void Download(string websitePath, string fileNumber)
        {
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", UserAgent);
            string reply = client.DownloadString(websitePath);

            FileUtils.WriteToFile(fileName, reply);
        }

        private static readonly HttpClient client = new HttpClient();

        private static async Task ProcessRepositoties()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent",".NET Foundation Repository Reporter");

            var stringTask = client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");

            var msg = await stringTask;
            Console.Write(msg);

        }

    }
}
