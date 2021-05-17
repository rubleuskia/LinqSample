using System;
using JobScheduler;

namespace AnalyticsProgram.Jobs
{
    public abstract class BaseJob : IJob
    {
        private bool _isFailed;

        public abstract void Execute(DateTime signalTime);

        public virtual bool ShouldRun(DateTime signalTime)
        {
            return !_isFailed;
        }

        public virtual void MarkAsFailed()
        {
            _isFailed = true;
        }
    }
}
