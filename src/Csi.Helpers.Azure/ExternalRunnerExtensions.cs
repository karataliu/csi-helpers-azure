using System.Threading.Tasks;

namespace Csi.Helpers.Azure
{
    public static class ExternalRunnerExtensions
    {
        public static Task<int> RunPowershell(this IExternalRunner runner, string script)
            => runner.RunExecutable("powershell", "-Command", script);

        public static Task<int> RunBash(this IExternalRunner runner, string script)
            => runner.RunExecutable("bash", "-c", "\"" + script.Replace("\\", "\\\\") + "\"");
    }
}
