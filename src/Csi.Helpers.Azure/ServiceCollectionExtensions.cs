using Microsoft.Extensions.DependencyInjection;

namespace Csi.Helpers.Azure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExternalRunner(this IServiceCollection sc)
            => sc.AddSingleton<IExternalRunnerFactory, ExternalRunnerFactory>();

        public static IServiceCollection AddInstanceMetadataService(this IServiceCollection sc)
            => sc.AddSingleton<IInstanceMetadataService, InstanceMetadataService>();
    }
}
