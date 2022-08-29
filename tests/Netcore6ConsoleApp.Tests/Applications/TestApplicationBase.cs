using System.Threading.Tasks;

namespace Netcore6ConsoleApp.Tests.Applications
{
    /// <summary>
    /// テストアプリケーションのベース
    /// </summary>
    public abstract class TestApplicationBase
    {
        /// <summary>
        /// コマンドラインアプリケーションを実行する
        /// </summary>
        /// <param name="args">コマンドライン引数</param>
        /// <returns></returns>
        public abstract Task<int> RunAsync(string[] args);
    }
}
