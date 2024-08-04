using BenchmarkDotNet.Attributes;

namespace PerfTester;

public class CallVsCallvirtBenchmark
{
    public string Method() => "call";
    
    public virtual string MethodVirtual() => "callvirt";

    [Benchmark(Baseline = true)]
    public string CallMethod()
    {
        return Method();
    }

    [Benchmark]
    public string CallVirtualMethod()
    {
        return MethodVirtual();
    }
}