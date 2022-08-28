using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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

        public ZipLogic(ILogger<ZipLogic> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public Task RunAsync(string zipCode)
        {
            _logger.LogInformation($"start search zip code: {zipCode}");

            return Task.CompletedTask;
        }
    }
}
