namespace AnalyticsAdapter
{
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
        CustomerOverView GetCustomerOverview(int customerId);
        List<(string productName, int numberOfPurchases)> GetProductsPurchased(int customerId);
    }
}
