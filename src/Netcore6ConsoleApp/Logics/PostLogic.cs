using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Netcore6ConsoleApp.Logics
{
    /// <summary>
    /// postロジックのインターフェース
    /// </summary>
    public interface IPostLogic
    {
        /// <summary>
        /// postロジックの実行
        /// </summary>
        /// <param name="postCode">検索する郵便番号</param>
        /// <returns></returns>
        Task RunAsync(string postCode);
    }

    /// <summary>
    /// postロジックの実装
    /// </summary>
    public class PostLogic : IPostLogic
    {
        private readonly ILogger<PostLogic> _logger;
        private readonly IConfiguration _configuration;

        public PostLogic(ILogger<PostLogic> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public Task RunAsync(string postCode)
        {
            _logger.LogInformation($"start search post code: {postCode}");

            return Task.CompletedTask;
        }
    }
}
