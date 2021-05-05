using AnalyticsAdapter;

namespace AnalyticsProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            var adapter = new AnalyticsAdapter();
            var db = new Database();
            adapter.Connect(new Repository(db));
        }
    }
}
