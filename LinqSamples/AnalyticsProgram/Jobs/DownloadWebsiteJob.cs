using System;
using AnalyticsProgram.Utils;

namespace AnalyticsProgram.Jobs
{
    public class DownloadWebsiteJob : BaseJob
    {
        private const string FilePath = "Stackoverflow.txt";
        private const string WebsitePath = "https://stackoverflow.com/questions/26233/fastest-c-sharp-code-to-download-a-web-page";

        public override void Execute(DateTime signalTime)
        {
            WebsiteUtils.Download(WebsitePath, FilePath);
        }
    }
}
