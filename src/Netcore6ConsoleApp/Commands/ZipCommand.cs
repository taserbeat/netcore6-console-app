using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using Netcore6ConsoleApp.Logics;

namespace Netcore6ConsoleApp.Commands
{
    /// <summary>
    /// zipコマンド
    /// </summary>
    [Command("zip")]
    public class ZipCommand : CommandBase
    {
        private readonly ILogger<ZipCommand> _logger;
        private readonly IZipLogic _zipLogic;

        [Argument(0, "-c|--code", "検索する郵便番号: XXX-XXXX")]
        public string? ZipCode { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="zipLogic"></param>
        public ZipCommand(ILogger<ZipCommand> logger, IZipLogic zipLogic)
        {
            _logger = logger;
            _zipLogic = zipLogic;
        }

        public override async Task OnExecuteAsync(CommandLineApplication app)
        {
            if (string.IsNullOrWhiteSpace(ZipCode))
            {
                var exception = new ArgumentException();
                _logger.LogError("郵便番号が指定されていません", exception);
                throw exception;
            }

            await _zipLogic.RunAsync(ZipCode);
        }
    }
}
