using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobScheduler
{
    class JobExecutionTimeInConsole:IJob
    {

        public bool IsFailed { get; set; }

        public DateTime StartJobAt { get; set; }

        public JobExecutionTimeInConsole() : this(DateTime.MinValue)
        {

        }

        public JobExecutionTimeInConsole(DateTime timeStart)
        {
            StartJobAt = timeStart;
        }

        public void Execute(DateTime signalTime)
        {
            Console.WriteLine($"Executed: {signalTime}");
        }
    }
}
