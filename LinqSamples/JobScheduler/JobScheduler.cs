using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;

namespace JobScheduler
{
    public class JobScheduler
    {
        private readonly Timer _timer;
        private readonly List<IJob> _jobs = new();
        private readonly List<IDelayedJob> _delayedJobs = new();
        private CancellationTokenSource _tokenSource;
        private IConsoleWrapper _console = new ConsoleWrapper();

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

        public void CancelJobs()
        {
            _tokenSource.Cancel();
        }

        public void Start()
        {
            _tokenSource = new CancellationTokenSource();
            _timer.Start();
        }

        public void Stop()
        {
            CancelJobs();
            _timer.Stop();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs @event)
        {
            if (_tokenSource.Token.IsCancellationRequested)
            {
                _tokenSource = new CancellationTokenSource();
            }

            OnTimedEventAsync(@event).GetAwaiter().GetResult();
        }

        private async Task OnTimedEventAsync(ElapsedEventArgs @event)
        {
             var task1 = ExecuteSimpleJobs(@event);
             var task2 = ExecuteDelayedJobs(@event);

             await Task.WhenAll(task1, task2);
        }

        private async Task ExecuteSimpleJobs(ElapsedEventArgs @event)
        {
            await ExecuteJobs(_jobs, @event.SignalTime);
        }

        private async Task ExecuteDelayedJobs(ElapsedEventArgs @event)
        {
            await ExecuteJobs(_delayedJobs.Select(x => x as IJob), @event.SignalTime);
        }

        private async Task ExecuteJobs(IEnumerable<IJob> jobs, DateTime startAt)
        {
            foreach (var job in jobs)
            {
                if (await job.ShouldRun(startAt))
                {
                    await ExecuteJob(job, startAt);
                }
            }
        }

        private async Task ExecuteJob(IJob job, DateTime signalTime)
        {
            try
            {
                await job.Execute(signalTime, _tokenSource.Token);
            }
            catch (OperationCanceledException e)
            {
                _console.WriteLine($"Operation was cancelled with exception. {e.Message}");
            }

            catch (Exception e)
            {
                _console.WriteLine(e.Message);
                job.MarkAsFailed();
            }
        }
    }
}
