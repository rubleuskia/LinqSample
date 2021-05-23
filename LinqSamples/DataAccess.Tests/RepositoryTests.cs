using System;
using System.Collections;
using AnalyticsAdapter;
using FluentAssertions;
using Xunit;

namespace DataAccess.Tests
{
    public class RepositoryTests
    {
        // AAA - triple A tests
        [Fact]
        public void GetOrders_ForNonExistingCustomer_ReturnsEmptyResult()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);
            

            // act
            var orders = repository.GetOrders(10000);
            
            // assert
            Assert.Empty(orders);
        }
        
        [Fact]
        public void GetOrders_ForExistingCustomer_ReturnsResult()
        {
            // arrange
            var db = new FakeDatabase();
            db.Orders.Add(new Order(1,1,1));
            var repository = new Repository(db);

            // act
            var orders = repository.GetOrders(1);
            
            // assert
            orders.Should().BeEquivalentTo(new Order(1, 1, 1));
        }
        
        [Fact]
        public void AddOrder_Always_AddedSuccessfully()
        {
            // arrange
            var db = new FakeDatabase();
            var repository = new Repository(db);
            var countBefore = db.Orders.Count;

            // act
            repository.AddOrder(1, 1);
            
            // assert
            var countAfter = db.Orders.Count;
            countAfter.Should().Be(countBefore + 1);
        }
    }
}