using System;
using System.Threading.Tasks;
using Moq;
using Netcore6ConsoleApp.Tests.Applications;
using Xunit;
using Xunit.Abstractions;

namespace Netcore6ConsoleApp.Tests.CallTests
{
    /// <summary>
    /// zipコマンドのロジック呼び出しテスト
    /// </summary>
    public class ZipTests : IClassFixture<TestApplication>
    {
        private readonly TestApplication _app;

        public ZipTests(TestApplication app, ITestOutputHelper testOutputhelper)
        {
            _app = app;
            _app.TestOutputHelper = testOutputhelper;
            _app.Reset();
        }

        [Fact(DisplayName = "引数なしでの実行でエラー")]
        public async void FailureWithoutAnyArgs()
        {
            // 準備

            // 実行
            var args = new string[] { "zip" };
            await Assert.ThrowsAsync<ArgumentException>(async () => await _app.RunAsync(args));
        }

        [Fact(DisplayName = "zipCode引数ありで実行が正常")]
        public async Task SuccessWithZipCode()
        {
            // 準備
            var zipCode = "123-4567";

            // 実行
            var args = new string[] { "zip", zipCode };
            await _app.RunAsync(args);

            // 検証
            _app.ZipLogicMock.Verify(x => x.RunAsync(zipCode), Times.Once);
        }
    }
}
