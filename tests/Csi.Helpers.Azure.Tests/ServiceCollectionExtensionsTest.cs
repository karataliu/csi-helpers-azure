using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Csi.Helpers.Azure.Tests
{
    public class ServiceCollectionExtensionsTest
    {
        [Fact]
        public void ServiceCollectionAddExternalRunner()
        {
            var sp = new ServiceCollection()
                .AddLogging()
                .AddExternalRunner()
                .BuildServiceProvider();
            Assert.NotNull(sp.GetRequiredService<IExternalRunnerFactory>());
        }

        [Fact]
        public void ServiceCollectionAddInstanceMetadataService()
        {
            var sp = new ServiceCollection()
                .AddLogging()
                .AddInstanceMetadataService()
                .BuildServiceProvider();
            Assert.NotNull(sp.GetRequiredService<IInstanceMetadataService>());
        }
    }
}
