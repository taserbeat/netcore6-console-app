using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Moq;
using Netcore6ConsoleApp.Commands;
using Netcore6ConsoleApp.Logics;
using Xunit.Abstractions;

namespace Netcore6ConsoleApp.Tests.Applications
{
    /// <summary>
    /// Mockを使用したテスト用アプリケーション
    /// </summary>
    public class TestApplication : TestApplicationBase
    {
        public ITestOutputHelper? TestOutputHelper;

        public Mock<IRootLogic> RootLogicMock { get; set; } = new();

        public Mock<IZipLogic> ZipLogicMock { get; set; } = new();

        public IConfigurationRoot Configuration { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TestApplication()
        {
            Configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
        }

        public void Reset()
        {
            var dirName = Path.GetDirectoryName(typeof(TestApplication).Assembly.Location);

            RootLogicMock = new Mock<IRootLogic>();
            ZipLogicMock = new Mock<IZipLogic>();
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
                loggingBuilder.ClearProviders();
            })
            .ConfigureServices(services =>
            {
                services.AddScoped(_ => RootLogicMock.Object);
                services.AddScoped(_ => ZipLogicMock.Object);
                services.AddSingleton<IConfiguration>(_ => Configuration);
            });

            return hostBuilder.RunCommandLineApplicationAsync<RootCommand>(args);
        }
    }
}
