using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N6CA.Services.Models;
using Xunit;

namespace N6CA.Services.Tests.ZipInformationServiceTests
{
    public class GetPostInformationTests
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;

        public GetPostInformationTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()
                .Build();

            _services = new ServiceCollection();

            _services.AddLogging();
            _services.AddScoped<ZipInformationServiceMock>();
            _services.AddSingleton(_ => _configuration);
        }

        [Fact(DisplayName = "郵便番号を正しく検索できる")]
        public async Task Success()
        {
            // 準備
            const string zipCode = "123-4567";

            var zipInformation = new ZipInformation()
            {
                ZipCode = zipCode,
                Address = "北海道札幌市",
            };

            // 実行
            using var provider = _services.BuildServiceProvider();

            var service = provider.GetRequiredService<ZipInformationServiceMock>();
            var actualZipInformation = await service.GetPostInformation(zipCode);

            // 検証
            Assert.Equal(zipInformation.ZipCode, actualZipInformation.ZipCode);
            Assert.Equal(zipInformation.Address, actualZipInformation.Address);
        }
    }
}
