using System;

namespace AnalyticsProgram.Jobs
{
    public class LogExecutionTimeInConsoleJob : BaseJob
    {
        public override void Execute(DateTime signalTime)
        {
            Console.WriteLine($"Executed: {signalTime}");
        }
    }
}
