using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;
using Netcore6ConsoleApp.Logics;

namespace Netcore6ConsoleApp.Commands
{
    /// <summary>
    /// rootコマンド
    /// </summary>
    [Command("n6ca")]
    [Subcommand(typeof(ZipCommand))]
    public class RootCommand : CommandBase
    {
        private readonly ILogger<RootCommand> _logger;
        private readonly IRootLogic _rootLogic;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="rootLogic"></param>
        public RootCommand(ILogger<RootCommand> logger, IRootLogic rootLogic)
        {
            _logger = logger;
            _rootLogic = rootLogic;
        }

        public override async Task OnExecuteAsync(CommandLineApplication app)
        {
            await _rootLogic.RunAsync();
        }
    }
}
