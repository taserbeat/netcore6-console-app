using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using N6CA.Services;

namespace Netcore6ConsoleApp.Logics
{
    /// <summary>
    /// zipロジックのインターフェース
    /// </summary>
    public interface IZipLogic
    {
        /// <summary>
        /// zipロジックの実行
        /// </summary>
        /// <param name="zipCode">検索する郵便番号</param>
        /// <returns></returns>
        Task RunAsync(string zipCode);
    }

    /// <summary>
    /// zipロジックの実装
    /// </summary>
    public class ZipLogic : IZipLogic
    {
        private readonly ILogger<ZipLogic> _logger;
        private readonly IConfiguration _configuration;
        private readonly IZipInformationService _zipInformationService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="zipInformationService"></param>
        public ZipLogic(ILogger<ZipLogic> logger, IConfiguration configuration, IZipInformationService zipInformationService)
        {
            _logger = logger;
            _configuration = configuration;
            _zipInformationService = zipInformationService;
        }

        public async Task RunAsync(string zipCode)
        {
            _logger.LogInformation($"start search zip code: {zipCode}");

            var zipInformation = await _zipInformationService.GetPostInformation(zipCode);

            Console.WriteLine($"ZipCode: {zipInformation.ZipCode}{Environment.NewLine}Address: {zipInformation.Address}");

            return;
        }
    }
}
