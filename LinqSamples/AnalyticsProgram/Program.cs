namespace AnalyticsProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var adapter = new AnalyticsAdapter();
            adapter.Connect(null);
        }
    }
}
