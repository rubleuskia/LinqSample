using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions.Extensions;
using JobScheduler;
using Moq;
using Xunit;

namespace DataAccess.Tests
{
    public class JobSchedulerTests
    {
        [Fact]
        public async Task Start_ShouldRun_ExecutesJob()
        {
            // arrange
            var wrapperMock = new Mock<IConsoleWrapper>();
            var mockedDateTime = new Mock<IDateTimeProvider>();
            var mockedJob = new Mock<IJob>();

            mockedDateTime.Setup(x => x.Now).Returns(12.January(2012).At(9, 00));

            var scheduler = new JobScheduler.JobScheduler(200);
            scheduler.RegisterJob(mockedJob.Object);

            // act
            scheduler.Start();
            await Task.Delay(250);

            // assert
            // mockedJob.Verify(x => x.Execute(), Times.Once);
            wrapperMock.Verify(x => x.WriteLine("expected string"), Times.Once);
        }
        //
        // [Fact]
        // public async Task Start_ShouldNotRun_DoesNotExecuteJob()
        // {
        //     // arrange
        //     var mockedJob = new Mock<IJob>();
        //     mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).Returns(false);
        //
        //     var scheduler = new JobScheduler.JobScheduler(200);
        //     scheduler.RegisterJob(mockedJob.Object);
        //
        //     // act
        //     scheduler.Start();
        //     await Task.Delay(250);
        //
        //     // assert
        //     mockedJob.Verify(x => x.Execute(It.IsAny<DateTime>()), Times.Never);
        // }
        //
        // [Fact]
        // public async Task Start_ThrowsException_MarksJobAsFailed()
        // {
        //     // arrange
        //     var mockedJob = new Mock<IJob>();
        //     mockedJob.Setup(x => x.ShouldRun(It.IsAny<DateTime>())).Returns(true);
        //     mockedJob.Setup(x => x.Execute(It.IsAny<DateTime>())).Throws<Exception>();
        //
        //     var scheduler = new JobScheduler.JobScheduler(200);
        //     scheduler.RegisterJob(mockedJob.Object);
        //
        //     // act
        //     scheduler.Start();
        //     await Task.Delay(300);
        //
        //     // assert
        //     mockedJob.Verify(x => x.MarkAsFailed(), Times.AtLeast(1));
        // }
    }
}
