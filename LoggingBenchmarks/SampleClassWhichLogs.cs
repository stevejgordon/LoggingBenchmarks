using System;
using Microsoft.Extensions.Logging;

namespace LoggingBenchmarks
{
    public class SampleClassWhichLogs
    {
        private readonly ILogger _logger;

        public SampleClassWhichLogs(ILogger logger) => _logger = logger;

        public void DoSomethingWhichLogsOftenUsingLoggerMessage(string value1, int value2)
        {
            for (var i = 0; i < 1000; i++)
            {
                Log.InformationalMessage(_logger, value1, value2);
            }
        }

        public void DoSomethingWhichLogsOftenUsingLoggerMessageWithoutLevelCheck(string value1, int value2)
        {
            for (var i = 0; i < 1000; i++)
            {
                Log.DebugMessage(_logger, value1, value2);
            }
        }

        public void DoSomethingWhichLogsOftenUsingLoggerMessageWithLevelCheck(string value1, int value2)
        {
            for (var i = 0; i < 1000; i++)
            {
                Log.DebugMessageWithLevelCheck(_logger, value1, value2);
            }
        }

        public static class Log
        {
            public static class Events
            {
                public static readonly EventId Started = new EventId(100, "Started");
            }

            private static readonly Action<ILogger, string, int, Exception> _informationLoggerMessage = LoggerMessage.Define<string, int>(
                LogLevel.Information,
                Log.Events.Started,
                "This is a message with two params! {Param1}, {Param2}");

            public static void InformationalMessage(ILogger logger, string value1, int value2)
            {
                _informationLoggerMessage(logger, value1, value2, null);
            }

            private static readonly Action<ILogger, string, int, Exception> _debugLoggerMessage = LoggerMessage.Define<string, int>(
                LogLevel.Debug,
                Log.Events.Started,
                "This is a message with two params! {Param1}, {Param2}");

            public static void DebugMessage(ILogger logger, string value1, int value2)
            {
                _debugLoggerMessage(logger, value1, value2, null);
            }

            public static void DebugMessageWithLevelCheck(ILogger logger, string value1, int value2)
            {
                if (logger.IsEnabled(LogLevel.Debug))
                    _debugLoggerMessage(logger, value1, value2, null);
            }
        }
    }

    public class SampleClassWhichLogsOriginal
    {
        private readonly ILogger _logger;

        public SampleClassWhichLogsOriginal(ILogger logger) => _logger = logger;

        public void LogOnce(string value1, int value2)
        {
            _logger.LogInformation("This is a message with two params! {Param1}, {Param2}", value1, value2);
        }

        public void DoSomethingWhichLogsOften(string value1, int value2)
        {
            for (var i = 0; i < 1000; i++)
            {
                _logger.LogInformation("This is a message with two params! {Param1}, {Param2}", value1, value2);
            }
        }
    }
}