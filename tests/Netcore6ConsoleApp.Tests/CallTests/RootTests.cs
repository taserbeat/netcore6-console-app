using System;
using System.Threading.Tasks;
using Moq;
using Netcore6ConsoleApp.Tests.Applications;
using Xunit;
using Xunit.Abstractions;

namespace Netcore6ConsoleApp.Tests.CallTests
{
    /// <summary>
    /// rootコマンドのロジック呼び出しテスト
    /// </summary>
    public class RootTests : IClassFixture<TestApplication>
    {
        private readonly TestApplication _app;

        public RootTests(TestApplication app, ITestOutputHelper testOutputHelper)
        {
            _app = app;
            _app.TestOutputHelper = testOutputHelper;
            _app.Reset();
        }

        [Fact(DisplayName = "引数なしでの実行が正常")]
        public async Task SuccessWithoutAnyArgs()
        {
            // 準備

            // 実行
            var args = Array.Empty<string>();
            await _app.RunAsync(args);

            // 検証
            _app.RootLogicMock.Verify(x => x.RunAsync(), Times.Once);
        }
    }
}
