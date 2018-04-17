using System;
using System.Threading.Tasks;
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
            var lf = new LoggerFactory();
            var runner = new ExternalRunner(showOutput, lf.CreateLogger<ExternalRunner>());
            await runner.RunPowershell("echo 1");
            await Assert.ThrowsAsync<Exception>(() => runner.RunPowershell("exit 1"));
            await Assert.ThrowsAnyAsync<Exception>(() => runner.RunExecutable("echo"));
        }
    }
}
