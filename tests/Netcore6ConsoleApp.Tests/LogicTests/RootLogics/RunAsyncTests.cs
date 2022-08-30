using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Netcore6ConsoleApp.Logics;

namespace Netcore6ConsoleApp.Tests.LogicTests.RootLogics
{
    /// <summary>
    /// RootロジックのRunAsyncのテスト
    /// </summary>
    public class RunAsyncTests
    {
        private readonly IServiceCollection _services;
        private readonly IConfigurationRoot _configuration;

        public RunAsyncTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();

            _services = new ServiceCollection();

            _services.AddLogging();
            _services.AddScoped<RootLogic>();
            _services.AddScoped<IConfiguration>(_ => _configuration);
        }
    }
}
