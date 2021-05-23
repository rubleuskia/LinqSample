using System.Collections.Generic;
using AnalyticsAdapter;

namespace DataAccess.Tests
{
    public class FakeDatabase : IDatabase
    {
        public List<Customer> Customers { get; set; } = new();
        public List<Order> Orders { get; set; } = new();
        public List<Product> Products { get; set; } = new();
    }
}