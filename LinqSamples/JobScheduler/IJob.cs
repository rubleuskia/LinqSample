using System;

namespace JobScheduler
{
    public interface IJob
    {
        void Execute(DateTime signalTime);
        bool ShouldRun(DateTime signalTime);
        void MarkAsFailed();
    }
}
