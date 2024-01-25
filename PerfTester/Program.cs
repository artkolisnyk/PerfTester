using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;
using PerfTester;

var config = DefaultConfig.Instance.WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest));

BenchmarkRunner.Run<LambdaExpressionVsMethodGroupBenchmark>(config);