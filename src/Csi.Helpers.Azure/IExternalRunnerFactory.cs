using Microsoft.Extensions.Logging;

namespace Csi.Helpers.Azure
{
    public interface IExternalRunnerFactory
    {
        IExternalRunner Create(bool showStdout, bool logArguments);
    }

    public sealed class ExternalRunnerConfig
    {
        public bool ShowStdout { get; set; }
        public bool LogArguments { get; set; }
    }

    class ExternalRunnerFactory : IExternalRunnerFactory
    {
        private readonly ILoggerFactory loggerFactory;

        public ExternalRunnerFactory(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        public IExternalRunner Create(bool showStdout, bool logArguments)
        {
            var config = new ExternalRunnerConfig
            {
                ShowStdout = showStdout,
                LogArguments = logArguments,
            };

            return new ExternalRunner(config, loggerFactory.CreateLogger<ExternalRunner>());
        }
    }
}
