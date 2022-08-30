using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Netcore6ConsoleApp.Commands;
using Xunit.Abstractions;

namespace Netcore6ConsoleApp.Tests.Applications
{
    /// <summary>
    /// Mockを使用しないテスト用アプリケーション
    /// </summary>
    public class TestApplicationWithoutMock : TestApplicationBase
    {
        public ITestOutputHelper? TestOutputHelper;

        public IConfiguration Configuration { get; set; }

        public TestApplicationWithoutMock()
        {
            Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();
        }

        /// <summary>
        /// テストテストアプリケーションを初期化する
        /// </summary>
        public void Reset()
        {
            var dirName = Path.GetDirectoryName(typeof(TestApplicationWithoutMock).Assembly.Location);
            Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .AddJsonFile(Path.Join(dirName, "appsettings.Test.json"), optional: true, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();
        }

        public override Task<int> RunAsync(string[] args)
        {
            var hostBuilder = Program.CreateHostBuilder();

            hostBuilder
                .ConfigureLogging(loggingBuilder =>
                {
                    // テスト実行中のログは不要なので無効にしておく
                    loggingBuilder.ClearProviders();
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IConfiguration>(_ => Configuration);
                });

            return hostBuilder.RunCommandLineApplicationAsync<RootCommand>(args);
        }
    }
}
