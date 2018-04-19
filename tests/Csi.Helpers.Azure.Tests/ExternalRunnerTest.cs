using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
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
            sc.AddExternalRunner()
              .AddLogging()
              .AddPlatformService<ITestCmdProvider>(psp =>
                psp.WithPlatform<TestCmdProviderWindows>(OSPlatform.Windows)
                    .WithPlatform<TestCmdProviderLinux>(OSPlatform.Linux));
            var sp = sc.BuildServiceProvider();
            var runnerFactory = sp.GetRequiredService<IExternalRunnerFactory>();
            var runner = runnerFactory.Create(showOutput, false);
            var cmdProvider = sp.GetRequiredService<ITestCmdProvider>();

            Assert.Equal(0, await cmdProvider.Echo(runner, "1"));
            Assert.Equal(0, await cmdProvider.Exit(runner, 0));
            Assert.Equal(2, await cmdProvider.Exit(runner, 2));
            await Assert.ThrowsAnyAsync<Exception>(() => cmdProvider.NonExistence(runner));
        }
    }

    interface ITestCmdProvider
    {
        Task<int> Echo(IExternalRunner runner, string text);
        Task<int> Exit(IExternalRunner runner, int exitCode);
        Task<int> NonExistence(IExternalRunner runner);
    }

    class TestCmdProviderWindows : ITestCmdProvider
    {
        public Task<int> Echo(IExternalRunner runner, string text) => runner.RunPowershell("echo " + text);
        public Task<int> Exit(IExternalRunner runner, int exitCode) => runner.RunPowershell("exit " + exitCode);
        public Task<int> NonExistence(IExternalRunner runner) => runner.RunExecutable("nonexistence");
    }

    class TestCmdProviderLinux : ITestCmdProvider
    {
        public Task<int> Echo(IExternalRunner runner, string text) => runner.RunExecutable("echo", text);
        public Task<int> Exit(IExternalRunner runner, int exitCode)
            => runner.RunExecutable("bash", "-c", "\"exit " + exitCode + "\"");
        public Task<int> NonExistence(IExternalRunner runner) => runner.RunExecutable("nonexistence");
    }
}
