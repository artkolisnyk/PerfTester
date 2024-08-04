using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using PerfTester.Mappers;

ManualConfig config = DefaultConfig.Instance.WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest));

BenchmarkRunner.Run<MappersBenchmark>(config);