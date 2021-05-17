using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace JobScheduler
{
    public class JobScheduler
    {
        private readonly Timer _timer;
        private readonly List<IJob> _jobs = new();
        private readonly List<IDelayedJob> _delayedJobs = new();

        public JobScheduler(int intervalMs)
        {
            _timer = new Timer(intervalMs);
            _timer.Elapsed += OnTimedEvent;
            _timer.AutoReset = true;
            _timer.Enabled = false;
        }

        public void RegisterJob(IJob job)
        {
            _jobs.Add(job);
        }

        public void RegisterJob(IDelayedJob job)
        {
            _delayedJobs.Add(job);
        }

        public void Start() => _timer.Start();
        public void Stop() => _timer.Stop();

        private void OnTimedEvent(object sender, ElapsedEventArgs @event)
        {
            ExecuteSimpleJobs(@event);
            ExecuteDelayedJobs(@event);
        }

        private void ExecuteSimpleJobs(ElapsedEventArgs @event)
        {
            ExecuteJobs(_jobs, @event.SignalTime);
        }

        private void ExecuteDelayedJobs(ElapsedEventArgs @event)
        {
            ExecuteJobs(_delayedJobs.Select(x => x as IJob), @event.SignalTime);
        }

        private void ExecuteJobs(IEnumerable<IJob> jobs, DateTime startAt)
        {
            foreach (var job in jobs.Where(x => x.ShouldRun(startAt)))
            {
                ExecuteJob(job, startAt);
            }
        }

        private void ExecuteJob(IJob job, DateTime signalTime)
        {
            try
            {
                job.Execute(signalTime);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                job.MarkAsFailed();
            }
        }
    }
}
