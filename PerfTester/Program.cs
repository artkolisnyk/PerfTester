using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using PerfTester.Mappers;

var config = DefaultConfig.Instance
    .WithOption(ConfigOptions.KeepBenchmarkFiles, false)
    .WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest));

BenchmarkRunner.Run<MappersBenchmark>(config);