namespace Netcore6ConsoleApp.Providers
{
    /// <summary>
    /// コンソールへの標準出力をラップするプロバイダー
    /// </summary>
    public interface IConsoleProvider
    {
        /// <summary>
        /// 標準出力
        /// </summary>
        /// <param name="value"></param>
        void WriteLine(string? value);
    }

    /// <summary>
    /// コンソールへの標準出力を実装するクラス
    /// </summary>
    public class ConsoleProvider : IConsoleProvider
    {
        public void WriteLine(string? value)
        {
            Console.WriteLine(value);
        }
    }
}
