using System.Threading.Tasks;

namespace Csi.Helpers.Azure
{
    public static class ExternalRunnerExtensions
    {
        public static Task RunExecutable(this IExternalRunner runner, string path, params string[] arguments)
            => runner.RunExecutable(path, string.Join(" ", arguments));

        public static Task RunPowershell(this IExternalRunner runner, string script)
            => runner.RunExecutable("powershell", "-Command", script);
    }
}
