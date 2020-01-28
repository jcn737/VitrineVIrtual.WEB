using NLog;
using NLog.Targets;
using NLog.Config;

namespace VitrineVirtual.WEB.Nlog
{
    public class LoggerHelp
    {
        public static void LogHelp()
        {
            // Step 1. Create configuration object 
            var config = new LoggingConfiguration();

            // Step 2. Create targets
            var consoleTarget = new ColoredConsoleTarget("target1")
            {
                Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}"
            };
            config.AddTarget(consoleTarget);

            var fileTarget = new FileTarget("target2")
            {
                FileName = "${basedir}/file.txt",
                Layout = "${longdate} ${level} ${message}  ${exception}"
            };
            config.AddTarget(fileTarget);


            // Step 3. Define rules
            config.AddRuleForOneLevel(LogLevel.Error, fileTarget); // only errors to file
            config.AddRuleForAllLevels(consoleTarget); // all to console

            // Step 4. Activate the configuration
            LogManager.Configuration = config;
        }
    }
}