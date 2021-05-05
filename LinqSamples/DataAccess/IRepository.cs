using System;
using System.Collections.Generic;
using System.Linq;

namespace AnalyticsAdapter
{
    public class Repository : IRepository
    {
        private readonly Database _db;

        public Repository(Database db)
        {
            _db = db;
        }

        public Order[] GetOrders(int customerId)
        {
            return _db.Orders
                .Where(order => order.CustomerId == customerId)
                .ToArray();
        }

        public Order GetOrder(int orderId)
        {
            var order = _db.Orders.SingleOrDefault(order1 => order1.Id == orderId);
            if (order == null)
            {
                throw new InvalidOperationException();
            }

            return order;
        }

        public decimal GetMoneySpentBy(int customerId)
        {
            return _db.Orders.Join(_db.Products,
                    (o) => o.ProductId,
                    (p) => p.Id,
                    (o, p) => new {p.Price, o.CustomerId})
                .Where(x => x.CustomerId == customerId)
                .Sum(x => x.Price);
        }

        public Product[] GetAllProductsPurchased(int customerId)
        {
            throw new NotImplementedException();
        }

        public CustomerOverview GetCustomerOverview(int customerId)
        {
            var name = _db.Customers.Single(x => x.Id == customerId).Name;

            return new CustomerOverview
            {
                Name = name,
                TotalMoneySpent = GetMoneySpentBy(customerId),
                FavoriteProductName = GetFavoriteProductName(customerId),
                //
            };
        }

        public List<(string productName, int numberOfPurchases)> GetProductsPurchased(int customerId)
        {
            throw new NotImplementedException();
        }

        private string GetFavoriteProductName(int customerId)
        {
            return GetOrders(customerId).Join(_db.Products,
                    (o) => o.ProductId,
                    (p) => p.Id,
                    (o, p) => new
                    {
                        ProductId = p.Id,
                        ProductName = p.Name,
                    })
                .GroupBy(x => x.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    Count = g.Count(),
                    ProductName = g.First().ProductName
                })
                .OrderBy(x => x.Count)
                .Last()
                .ProductName;
        }

        public bool AreAllPurchasesHigherThan(int customerId, decimal targetPrice)
        {
            return GetOrders(customerId)
                .Join(_db.Products,
                    (o) => o.ProductId,
                    (p) => p.Id,
                    (o, p) => new
                    {
                        p.Price,
                    })
                .All(x => x.Price > targetPrice);
        }

        public int GetTotalProductsPurchased(int productId)
        {
            throw new NotImplementedException();
        }

        public bool HasEverPurchasedProduct(int customerId, int productId)
        {
            return GetOrders(customerId).Any(x => x.ProductId == productId);
        }

        public Product[] GetUniqueProductsPurchased(int customerId)
        {
            return GetOrders(customerId)
                .Join(_db.Products, (o) => o.ProductId, (p) => p.Id, (o, p) => p)
                .Distinct()
                .ToArray();
        }

        public bool DidPurchaseAllProducts(int customerId, params int[] productIds)
        {
            // 1,1,2,2,3
            // 2,3
            return GetOrders(customerId)
                .Select(o => o.ProductId)
                .Distinct()
                .Intersect(productIds)
                .Count() == productIds.Count();
        }
    }

    public interface IRepository
    {
        Order[] GetOrders(int customerId);

        Order GetOrder(int orderId);

        decimal GetMoneySpentBy(int customerId);

        Product[] GetAllProductsPurchased(int customerId);

        Product[] GetUniqueProductsPurchased(int customerId);

        int GetTotalProductsPurchased(int productId);

        bool HasEverPurchasedProduct(int customerId, int productId);

        bool AreAllPurchasesHigherThan(int customerId, decimal targetPrice);

        bool DidPurchaseAllProducts(int customerId, params int[] productIds);

        CustomerOverview GetCustomerOverview(int customerId);

        List<(string productName, int numberOfPurchases)> GetProductsPurchased(int customerId);
    }
}
