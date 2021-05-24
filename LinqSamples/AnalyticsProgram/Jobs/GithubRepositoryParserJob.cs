using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using JobScheduler;

namespace AnalyticsProgram.Jobs
{
    public class GithubRepositoryParserJob : BaseJob
    {
        private readonly IConsoleWrapper _consoleWrapper;
        private static readonly HttpClient Client = new();

        public GithubRepositoryParserJob()
        {
            _consoleWrapper = new ConsoleWrapper();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            Client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
        }

        public override async Task Execute(DateTime signalTime, CancellationToken token)
        {
            // heavy request
            var result = await ProcessRepositories(token);

            if (token.IsCancellationRequested)
            {
                _consoleWrapper.WriteLine("GithubRepositoryParserJob: Operation cancelled.");
                return;
            }

            var repos = JsonSerializer.Deserialize<Repository[]>(result);

            // heavy results handling
            _consoleWrapper.WriteLine(result);
        }

        public override async Task<bool> ShouldRun(DateTime signalTime)
        {
            return await base.ShouldRun(signalTime);
        }

        private static async Task<string> ProcessRepositories(CancellationToken token)
        {
            var stringTask = Client.GetStringAsync("https://api.github.com/orgs/dotnet/repos", token);
            return await stringTask;
        }

        public class Repository
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("node_id")]
            public string NodeId { get; set; }

            [JsonPropertyName("name")]
            public string Name { get; set; }

            [JsonPropertyName("full_name")]
            public string FullName { get; set; }

            [JsonPropertyName("private")]
            public bool IsPrivate { get; set; }

            public bool IsPrivate { get; set; }
        }
    }
}
