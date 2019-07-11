using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using JetBrains.Profiler.Api;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LoggingBenchmarks
{
    internal class Program
    {
        private static void Main() => _ = BenchmarkRunner.Run<LoggingBenchmarks>();

        //private static void Main()
        //{
        //    IServiceCollection serviceCollection = new ServiceCollection();
        //    serviceCollection.AddLogging(builder => builder
        //        .AddFilter(level => level >= LogLevel.Information)
        //    );

        //    var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();

        //    var logger = loggerFactory.CreateLogger("TEST");

        //    var sut1 = new SampleClassWhichLogsOriginal(logger);
        //    var sut2 = new SampleClassWhichLogs(logger);

        //    const string hello = "Hello";
        //    const int ten = 10;

        //    MemoryProfiler.CollectAllocations(true);
            
        //    MemoryProfiler.GetSnapshot();

        //    sut1.LogOnce(hello, ten);

        //    MemoryProfiler.GetSnapshot();

        //    sut2.DoSomethingWhichLogsOftenUsingLoggerMessage(hello, ten);

        //    MemoryProfiler.GetSnapshot();
        //}
    }

    [MemoryDiagnoser]
    public class LoggingBenchmarks
    {
        private SampleClassWhichLogsOriginal _sut1;
        private SampleClassWhichLogs _sut2;

        private const string Value1 = "Value";
        private const int Value2 = 1000;

        private ILogger _logger;

        [GlobalSetup]
        public void Setup()
        {
            IServiceCollection serviceCollection = new ServiceCollection();

            serviceCollection.AddLogging(builder => builder
                .AddFilter(level => level >= LogLevel.Information)
            );

            var loggerFactory = serviceCollection.BuildServiceProvider().GetService<ILoggerFactory>();

            var logger = loggerFactory.CreateLogger("TEST");
            _logger = logger;

            _sut1 = new SampleClassWhichLogsOriginal(logger);
            _sut2 = new SampleClassWhichLogs(logger);
        }

        [Benchmark(Baseline = true)]
        public void LogDirect() => _sut1.DoSomethingWhichLogsOften(Value1, Value2);

        [Benchmark]
        public void LogViaStatic() => _sut2.DoSomethingWhichLogsOftenUsingLoggerMessage(Value1, Value2);

        //[Benchmark]
        //public void LogDebugWithoutLevelCheck() => _sut2.DoSomethingWhichLogsOftenUsingLoggerMessageWithoutLevelCheck(Value1, Value2);

        //[Benchmark]
        //public void LogDebugWithLevelCheck() => _sut2.DoSomethingWhichLogsOftenUsingLoggerMessageWithLevelCheck(Value1, Value2);
    }
}
