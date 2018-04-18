using Microsoft.Extensions.DependencyInjection;

namespace Csi.Helpers.Azure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExternalRunner(this IServiceCollection sc, bool showStdout = true)
            => sc.AddSingleton<IExternalRunner>(sp
                => ActivatorUtilities.CreateInstance<ExternalRunner>(sp, showStdout));

        public static IServiceCollection AddInstanceMetadataService(this IServiceCollection sc)
            => sc.AddSingleton<IInstanceMetadataService, InstanceMetadataService>();
    }
}
