using NLog;
using NLog.Config;
using NLog.Targets;

namespace ChronoFlow.Server;

internal static class Logging
{
    internal static Logger CreateLogger()
    {
        var loggingConfiguration = new LoggingConfiguration();
        loggingConfiguration.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, GetLogFileTarget());

        LogManager.Configuration = loggingConfiguration;
        LogManager.ThrowExceptions = true;

        return LogManager.GetCurrentClassLogger();
    }

    private static FileTarget GetLogFileTarget()
    {
        var logFileTarget = new FileTarget
        {
            Name = "logfile",
            FileName = "logs/log.txt",
            ArchiveDateFormat = "yyyy-MM-dd",
            ArchiveOldFileOnStartup = true,
            ArchiveNumbering = ArchiveNumberingMode.Date,
            MaxArchiveDays = 31,
            FileNameKind = FilePathKind.Relative,
        };

        return logFileTarget;
    }
}
