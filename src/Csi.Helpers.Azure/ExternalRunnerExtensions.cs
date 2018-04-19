using System.Threading.Tasks;

namespace Csi.Helpers.Azure
{
    public static class ExternalRunnerExtensions
    {
        public static Task RunPowershell(this IExternalRunner runner, string script)
            => runner.RunExecutable("powershell", "-Command", script);

        public static Task RunBash(this IExternalRunner runner, string script)
            => runner.RunExecutable("bash", "-c", "\"" + script.Replace("\\", "\\\\") + "\"");
    }
}
