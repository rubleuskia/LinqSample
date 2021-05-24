using System;
using System.Threading;
using AnalyticsProgram.Jobs;
using JobScheduler;
using Moq;

namespace DataAccess.Tests
{
    public class LogExecutionTimeInConsoleJobTests
    {
        public void Test()
        {
            // // mock console wrapper
            // var wrapperMock = new Mock<IConsoleWrapper>();
            //
            // var job = new GithubRepositoryParserJob(wrapperMock.Object);
            //
            // job.Execute(DateTime.Now, CancellationToken.None);
            //
            // // assert
            // //verify consle wrapper
        }
    }
}
