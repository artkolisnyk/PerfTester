using BenchmarkDotNet.Attributes;

namespace PerfTester;

[MemoryDiagnoser(false)]
public class LambdaExpressionVsMethodGroupBenchmark
{
    private int[] _ages;

    [Params(1000, 1_000_000)]
    public int Size { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        _ages = Enumerable.Range(0, Size).Select(x => Random.Shared.Next(1, 100)).ToArray();
    }
    
    [Benchmark(Baseline = true)]
    public int[] LambdaExpression()
    {
        return _ages.Where(x => FilterYoungerThan50(x)).ToArray();
    }
    
    [Benchmark]
    public int[] MethodGroup()
    {
        return _ages.Where(FilterYoungerThan50).ToArray();
    }

    private static bool FilterYoungerThan50(int age)
    {
        return age < 50;
    }
}