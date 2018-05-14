using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Util.Extensions.Logging.Step;

namespace Csi.Helpers.Azure
{
    sealed class ExternalRunner : IExternalRunner
    {
        private bool showStdout => config.ShowStdout;

        private readonly ExternalRunnerConfig config;
        private readonly ILogger logger;

        public ExternalRunner(ExternalRunnerConfig config, ILogger<ExternalRunner> logger)
        {
            this.config = config;
            this.logger = logger;
        }

        public async Task<int> RunExecutable(string path, params string[] arguments)
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
                // For mount, arguments may contain credential, log arguments if requested
                if (config.LogArguments) 
                    logger.LogDebug("Arguments: {0}", argumentsStr);

                // line edit
                
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
                //if (exitCode != 0) throw new Exception($"Executable {path} failed with {exitCode}");

                _s.Commit();
            }

            return exitCode;
        }
    }
}
