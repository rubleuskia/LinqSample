using System;
using System.Threading;
using System.Threading.Tasks;

namespace AnalyticsProgram.Jobs
{
    public class LogExecutionTimeInConsoleJob : BaseJob
    {
        public override Task Execute(DateTime signalTime, CancellationToken token)
        {
            Console.WriteLine($"Executed: {signalTime}");
            return Task.CompletedTask;
        }
    }
}
