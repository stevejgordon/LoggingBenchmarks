using Microsoft.Extensions.Logging;

namespace LoggingBenchmarks
{
    public class ClassUsingStandardLogging
    {
        private readonly ILogger _logger;

        public ClassUsingStandardLogging(ILogger logger) => _logger = logger;

        public void LogOnceWithNoParam() => 
            _logger.LogInformation("This is a message with two params!");

        public void LogOnceWithOneParam(string value1) => 
            _logger.LogInformation("This is a message with two params! {Param1}", value1);

        public void LogOnceWithTwoParams(string value1, int value2) => 
            _logger.LogInformation("This is a message with two params! {Param1}, {Param2}", value1, value2);

        public void LogDebugOnceWithTwoParams(string value1, int value2) => 
            _logger.LogDebug("This is a message with two params! {Param1}, {Param2}", value1, value2);
    }
}