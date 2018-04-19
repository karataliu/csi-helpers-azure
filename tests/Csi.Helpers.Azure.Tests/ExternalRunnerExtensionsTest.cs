using Moq;
using Xunit;

namespace Csi.Helpers.Azure.Tests
{
    public class ExternalRunnerExtensionsTest
    {
        [Fact]
        public void RunPowershell()
        {
            var externalRunner = new Mock<IExternalRunner>();
            externalRunner.Setup(p => p.RunExecutable(It.IsAny<string>(), It.IsAny<string>()));
            externalRunner.Object.RunPowershell("Write-Output a;Write-Output b;");
            externalRunner.Verify(p => p.RunExecutable("powershell", "-Command", "Write-Output a;Write-Output b;"));
            externalRunner.VerifyNoOtherCalls();
        }

        [Fact]
        public void RunBash()
        {
            var externalRunner = new Mock<IExternalRunner>();
            externalRunner.Setup(p => p.RunExecutable(It.IsAny<string>(), It.IsAny<string>()));
            externalRunner.Object.RunBash("echo a;echo b;");
            externalRunner.Verify(p => p.RunExecutable("bash", "-c", "\"echo a;echo b;\""));
            externalRunner.VerifyNoOtherCalls();
        }
    }
}
