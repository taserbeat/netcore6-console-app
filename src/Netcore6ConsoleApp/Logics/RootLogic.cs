using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Netcore6ConsoleApp.Logics
{
    /// <summary>
    /// rootロジックのインターフェース
    /// </summary>
    public interface IRootLogic
    {
        /// <summary>
        /// rootロジックの実行
        /// </summary>
        /// <returns></returns>
        Task RunAsync();
    }

    /// <summary>
    /// rootロジックの実装
    /// </summary>
    public class RootLogic : IRootLogic
    {
        private readonly ILogger<RootLogic> _logger;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        public RootLogic(ILogger<RootLogic> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public Task RunAsync()
        {
            _logger.LogInformation("start root logic");

            return Task.CompletedTask;
        }
    }
}
