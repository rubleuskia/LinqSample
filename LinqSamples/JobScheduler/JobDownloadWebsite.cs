using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AnalyticsProgram
{
    class JobDownloadWebsite:IJob
    {
        private readonly string _path;

        public bool IsFailed { get; set; }

        public DateTime StartJobAt { get; set; }

        public JobDownloadWebsite(string path) : this(path, DateTime.MinValue)
        {

        }

        public JobDownloadWebsite(string path, DateTime timeStart)
        {
            _path = "https://" + path.Replace("https://", "");
            StartJobAt = timeStart;
        }

        public void Execute(DateTime signalTime)
        {
            var client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            string reply = client.DownloadString(_path);

            var name = _path.Replace("https://", "") + ".txt";
            FileUtils.WriteToFile(name, reply);
        }
    }
}
