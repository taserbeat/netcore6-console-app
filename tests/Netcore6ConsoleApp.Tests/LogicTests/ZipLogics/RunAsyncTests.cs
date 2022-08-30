using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using N6CA.Services;
using N6CA.Services.Models;
using Netcore6ConsoleApp.Logics;
using Xunit;

namespace Netcore6ConsoleApp.Tests.LogicTests.ZipLogics
{
    /// <summary>
    /// ZipLogicのRunAsyncのテスト
    /// </summary>
    public class RunAsyncTests
    {
        private readonly IServiceCollection _services;
        private readonly IConfigurationRoot _configuration;

        private readonly Mock<IZipInformationService> _zipInformationMock;

        public RunAsyncTests()
        {
            _zipInformationMock = new Mock<IZipInformationService>();

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();

            _services = new ServiceCollection();

            _services.AddLogging();
            _services.AddScoped<ZipLogic>();
            _services.AddScoped(_ => _zipInformationMock.Object);
            _services.AddScoped<IConfiguration>(_ => _configuration);
        }

        [Fact(DisplayName = "郵便番号が service に正しく渡る")]
        public async Task Success()
        {
            // 準備
            const string zipCode = "123-4567";

            var dummyZipInformation = new ZipInformation()
            {
                ZipCode = zipCode,
                Address = "北海道札幌市",
            };

            _zipInformationMock
                .Setup(x => x.GetPostInformation(It.IsAny<string>()))
                .Returns(Task.FromResult(dummyZipInformation));

            await using var provider = _services.BuildServiceProvider();
            var zipLogic = provider.GetRequiredService<ZipLogic>();

            // 実行
            await zipLogic.RunAsync(zipCode);

            // 検証
            _zipInformationMock.Verify(x => x.GetPostInformation(zipCode), Times.Once);
        }
    }
}
