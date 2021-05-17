using System;
using AnalyticsProgram.Utils;

namespace AnalyticsProgram.Jobs
{
    public class WebsiteDownloadDelayedJob : BaseDelayedJob
    {
        public WebsiteDownloadDelayedJob(DateTime startAt) : base(startAt)
        {
        }

        public override void Execute(DateTime signalTime)
        {
            base.Execute(signalTime);
            WebsiteUtils.Download("https://tut.by", "tutby.txt");
        }
    }
}
