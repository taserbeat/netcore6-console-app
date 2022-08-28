using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using N6CA.Services;
using Netcore6ConsoleApp.Commands;
using Netcore6ConsoleApp.Logics;
using NLog.Extensions.Logging;

namespace Netcore6ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var builder = CreateHostBuilder();
            builder.RunCommandLineApplicationAsync<RootCommand>(args);
        }

        static IHostBuilder CreateHostBuilder()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureHostConfiguration(builder =>
                {
                    var dirName = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                    builder
                        .AddJsonFile(Path.Join(dirName, "appsettings.json"), optional: true, reloadOnChange: false)
                        .AddJsonFile(Path.Join(dirName, "appsettings.Development.json"), optional: true, reloadOnChange: false);

                    builder.AddEnvironmentVariables();
                })
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.ClearProviders();
                    // .NET Coreのロガーをコンソール出力したい場合は次のコメントアウトを解除する
                    // loggingBuilder.AddConsole();
                    loggingBuilder.AddNLog();
                    loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                })
                .ConfigureServices(services =>
                {
                    services.AddScoped<IRootLogic, RootLogic>();
                    services.AddScoped<IZipLogic, ZipLogic>();
                    services.AddHttpClient<IZipInformationService, ZipCloudService>();
                    services.AddScoped<IZipInformationService, ZipCloudService>();
                });

            return hostBuilder;
        }
    }
}
