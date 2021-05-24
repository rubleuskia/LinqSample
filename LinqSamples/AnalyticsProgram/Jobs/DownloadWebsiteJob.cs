using System;
using System.Threading;
using System.Threading.Tasks;
using AnalyticsProgram.Utils;

namespace AnalyticsProgram.Jobs
{
    public class DownloadWebsiteJob : BaseJob
    {
        private const string FilePath = "Stackoverflow.txt";
        private const string WebsitePath = "https://stackoverflow.com/questions/26233/fastest-c-sharp-code-to-download-a-web-page";

        public override Task Execute(DateTime signalTime, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return Task.CompletedTask;
            }

            WebsiteUtils.Download(WebsitePath, FilePath);
            return Task.CompletedTask;
        }
    }
}
