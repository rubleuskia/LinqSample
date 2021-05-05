using AnalyticsAdapter;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace DataAccessTests
{
    public class RepositoryTests
    {
        [Fact]
        public void Test()
        {
            var mock = new Mock<IDatabase>();
            mock.Setup(foo => foo.Customers).Returns(new List<Customer>
            {
                new Customer(1, "John")
            });

            mock.Setup(foo => foo.Orders).Returns(new List<Order>
            {
                new Order(2, 2, 2)
            });


            var sut = new Repository(mock.Object);
            var result = sut.GetOrders(1);

            Assert.Empty(result);
        }

        [Theory]
        [InlineData(1, 10)]
        [InlineData(2, 20)]
        public void TestTheory(int customerId, int count)
        {
            var mock = new Mock<IDatabase>();
            mock.Setup(foo => foo.Customers).Returns(new List<Customer>
            {
                new Customer(1, "John")
            });

            mock.Setup(foo => foo.Orders).Returns(new List<Order>
            {
                new Order(2, 2, 2)
            });


            var sut = new Repository(mock.Object);
            var result = sut.GetOrders(customerId);

            result.Length.Should().Be(count);
        }
    }
}
