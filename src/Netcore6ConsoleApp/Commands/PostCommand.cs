using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using Netcore6ConsoleApp.Logics;

namespace Netcore6ConsoleApp.Commands
{
    /// <summary>
    /// postコマンド
    /// </summary>
    [Command("post")]
    public class PostCommand : CommandBase
    {
        private readonly ILogger<PostCommand> _logger;
        private readonly IPostLogic _postLogic;

        [Argument(0, "-c|--code", "検索する郵便番号: XXX-XXXX")]
        public string? PostCode { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="postLogic"></param>
        public PostCommand(ILogger<PostCommand> logger, IPostLogic postLogic)
        {
            _logger = logger;
            _postLogic = postLogic;
        }

        public override async Task OnExecuteAsync(CommandLineApplication app)
        {
            if (string.IsNullOrWhiteSpace(PostCode))
            {
                var exception = new ArgumentException();
                _logger.LogError("郵便番号が指定されていません", exception);
                throw exception;
            }

            await _postLogic.RunAsync(PostCode);
        }
    }
}
