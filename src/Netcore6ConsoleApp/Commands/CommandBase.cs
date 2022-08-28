using McMaster.Extensions.CommandLineUtils;

namespace Netcore6ConsoleApp.Commands
{
    /// <summary>
    /// コマンドクラスのベース
    /// </summary>
    public abstract class CommandBase
    {
        /// <summary>
        /// コマンド呼び出し時の処理
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public abstract Task OnExecuteAsync(CommandLineApplication app);
    }
}
