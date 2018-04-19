using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Csi.Helpers.Azure.Tests
{
    public class ExternalRunnerTest
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public async Task RunExecutable(bool showOutput)
        {
            var sc = new ServiceCollection();
            sc.AddExternalRunner(showOutput)
              .AddLogging()
              .AddPlatformService<ITestCmdProvider>(psp =>
                psp.WithPlatform<TestCmdProviderWindows>(OSPlatform.Windows)
                    .WithPlatform<TestCmdProviderLinux>(OSPlatform.Linux));
            var sp = sc.BuildServiceProvider();
            var runner = sp.GetRequiredService<IExternalRunner>();
            var cmdProvider = sp.GetRequiredService<ITestCmdProvider>();

            await cmdProvider.Echo(runner, "1");
            await cmdProvider.Exit(runner, 0);
            await Assert.ThrowsAsync<Exception>(() => cmdProvider.Exit(runner, 2));
            await Assert.ThrowsAnyAsync<Exception>(() => cmdProvider.NonExistence(runner));
        }
    }

    interface ITestCmdProvider
    {
        Task Echo(IExternalRunner runner, string text);
        Task Exit(IExternalRunner runner, int exitCode);
        Task NonExistence(IExternalRunner runner);
    }

    class TestCmdProviderWindows : ITestCmdProvider
    {
        public Task Echo(IExternalRunner runner, string text) => runner.RunPowershell("echo " + text);
        public Task Exit(IExternalRunner runner, int exitCode) => runner.RunPowershell("exit " + exitCode);
        public Task NonExistence(IExternalRunner runner) => runner.RunExecutable("nonexistence");
    }

    class TestCmdProviderLinux : ITestCmdProvider
    {
        public Task Echo(IExternalRunner runner, string text) => runner.RunExecutable("echo", text);
        public Task Exit(IExternalRunner runner, int exitCode)
            => runner.RunExecutable("bash", "-c", "\"exit " + exitCode + "\"");
        public Task NonExistence(IExternalRunner runner) => runner.RunExecutable("nonexistence");
    }
}
