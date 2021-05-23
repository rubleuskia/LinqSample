using System.Collections.Generic;
using System.Linq;

namespace AnalyticsAdapter
{
    public class Repository : IRepository
    {
        private readonly IDatabase _db;

        public Repository(IDatabase db)
        {
            _db = db;
        }
        
        public Order[] GetOrders(int customerId)
        {
            return _db.Orders.Where(order => order.CustomerId == customerId).ToArray();
        }

        public Order GetOrder(int orderId)
        {
            return _db.Orders.SingleOrDefault(order => order.Id == orderId);
        }

        public decimal GetMoneySpentBy(int customerId)
        {
            return _db.Orders.Where(order => order.CustomerId == customerId)
                .Join(_db.Products, 
                    order => order.ProductId, 
                    product => product.Id, 
                    (order, product) => product.Price)
                .Sum(price => price);
        }

        public Product[] GetAllProductsPurchased(int customerId)
        {
            return _db.Orders.Where(order => order.CustomerId == customerId)
                .Join(_db.Products, 
                    order => order.ProductId,
                    product => product.Id,
                    (order, product) => product).ToArray();
        }

        public Product[] GetUniqueProductsPurchased(int customerId)
        {
            return _db.Orders.Where(order => order.CustomerId == customerId)
                .Select(order => order.ProductId)
                .Distinct()
                .Join(_db.Products,
                    id => id,
                    product => product.Id,
                    (id,product) => product).ToArray();
        }

        public int GetTotalProductsPurchased(int productId)
        {
            return _db.Orders.Count(order => order.ProductId == productId);
        }

        public bool HasEverPurchasedProduct(int customerId, int productId)
        {
            return _db.Orders.Any(order => order.CustomerId == customerId && order.ProductId == productId);
        }

        public bool AreAllPurchasesHigherThan(int customerId, decimal targetPrice)
        {
            return _db.Orders.Where(order => order.CustomerId == customerId)
                .Join(_db.Products,
                    order => order.ProductId,
                    product => product.Id,
                    (order, product) => product)
                .All(product => product.Price > targetPrice);
        }

        public bool DidPurchaseAllProducts(int customerId, params int[] productIds)
        {
            return _db.Orders.Where(order => order.CustomerId == customerId)
                .Select(order => order.ProductId)
                .Distinct()
                .All(productIds.Contains);
        }

        public CustomerOverview GetCustomerOverview(int customerId)
        {
            var orders = _db.Orders.Where(order => order.CustomerId == customerId).ToList();
            
            var allPurchasedProducts = orders.Join(_db.Products,
                order => order.ProductId,
                product => product.Id,
                (order, product) => product).ToList();
            
            var name = _db.Customers.SingleOrDefault(customer => customer.Id == customerId)?.Name;
            var totalProductsPurchased = orders.Count;
            
            var favoriteProductName = allPurchasedProducts.GroupBy(x => x.Id)
                .Select(g => new
                {
                    ProductId = g.Key,
                    Count = g.Count(),
                    ProductName = g.First().Name
                })
                .OrderBy(x => x.Count)
                .Last()
                .ProductName;

            var maxAmountSpentPerProducts =
                allPurchasedProducts.OrderBy(product => product.Price).LastOrDefault().Price;
            var totalMoneySpent = allPurchasedProducts.Sum(product => product.Price);

            return new CustomerOverview()
            {
                Name = name,
                TotalProductsPurchased = totalProductsPurchased,
                FavoriteProductName = favoriteProductName,
                MaxAmountSpentPerProducts = maxAmountSpentPerProducts,
                TotalMoneySpent = totalMoneySpent
            };
        }

        public List<(string productName, int numberOfPurchases)> GetProductsPurchased(int customerId)
        {
            return _db.Orders.Where(order => order.CustomerId == customerId)
                .Join(_db.Products,
                    order => order.ProductId,
                    product => product.Id,
                    (order, product) => product)
                .GroupBy(product => product.Id)
                .Select(g => (g.First().Name, g.Count())).ToList();
        }

        public void AddOrder(int customerId, int productId)
        {
            _db.Orders.Add(new Order(_db.Orders.Count + 1, customerId, productId));
        }
    }
}