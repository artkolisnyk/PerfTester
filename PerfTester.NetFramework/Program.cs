using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;

namespace PerfTester.NetFramework
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var config = DefaultConfig.Instance.WithOrderer(new DefaultOrderer(SummaryOrderPolicy.FastestToSlowest));

            BenchmarkRunner.Run<CallVsCallvirtBenchmark>(config);
        }
    }
}