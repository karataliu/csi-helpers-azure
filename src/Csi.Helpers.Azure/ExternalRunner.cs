using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Util.Extensions.Logging.Step;

namespace Csi.Helpers.Azure
{
    sealed class ExternalRunner : IExternalRunner
    {
        private readonly bool showStdout;
        private readonly ILogger logger;

        public ExternalRunner(bool showStdout, ILogger<ExternalRunner> logger)
        {
            this.showStdout = showStdout;
            this.logger = logger;
        }

        public async Task RunExecutable(string path, params string[] arguments)
        {
            var argumentsStr = string.Join(" ", arguments);

            var info = new ProcessStartInfo
            {
                FileName = path,
                Arguments = argumentsStr,
                UseShellExecute = false,
                RedirectStandardOutput = !showStdout,
            };

            int exitCode = 0;
            using (var _s = logger.StepDebug("Run executable: {0}", path))
            {
                // For mount, arguments may contain credential, do not log arguments here
                await Task.Run(() =>
                {
                    using (var process = Process.Start(info))
                    {
                        if (!showStdout)
                        {
                            process.StandardOutput.ReadToEnd();
                        }
                        process.WaitForExit();
                        exitCode = process.ExitCode;
                    }
                });

                logger.LogDebug("Exit code: {0}", exitCode);
                if (exitCode != 0) throw new Exception($"Executable {path} failed with {exitCode}");

                _s.Commit();
            }
        }
    }
}
