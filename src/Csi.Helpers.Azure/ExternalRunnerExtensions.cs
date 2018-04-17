using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Csi.Helpers.Azure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExternalRunner(this IServiceCollection sc, bool showStdout = true)
            => sc.AddSingleton<IExternalRunner>(sp 
                => ActivatorUtilities.CreateInstance<ExternalRunner>(sp, showStdout));
    }

    public static class ExternalRunnerExtensions
    {
        public static Task RunExecutable(this IExternalRunner runner, string path, params string[] arguments)
            => runner.RunExecutable(path, string.Join(" ", arguments));

        public static Task RunPowershell(this IExternalRunner runner, string script)
            => runner.RunExecutable("powershell", "-Command", script);
    }
}
