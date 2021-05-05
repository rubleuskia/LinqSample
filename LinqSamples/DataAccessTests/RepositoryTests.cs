using AnalyticsAdapter;
using Moq;
using System;
using Xunit;

namespace DataAccessTests
{
    public class RepositoryTests
    {
        [Fact]
        public void Test()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(foo => foo.AreAllPurchasesHigherThan(1, 100))
                .Returns(true);

            // var db = new Database();
            // var repository = new Repository(db);
            var result = mock.Object.AreAllPurchasesHigherThan(1, 100);

            Assert.True(result);
        }
    }
}
