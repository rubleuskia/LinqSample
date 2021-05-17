using System;
using JobScheduler;

namespace AnalyticsProgram.Jobs
{
    public abstract class BaseDelayedJob : BaseJob, IDelayedJob
    {
        private bool _hasRun;
        private readonly DateTime _startAt;

        protected BaseDelayedJob(DateTime signalTime)
        {
            _startAt = signalTime;
        }

        public override void Execute(DateTime signalTime)
        {
            _hasRun = true;
        }

        public override bool ShouldRun(DateTime signalTime)
        {
            return base.ShouldRun(signalTime) && _startAt < signalTime && !_hasRun;
        }
    }
}