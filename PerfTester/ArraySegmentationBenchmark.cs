using BenchmarkDotNet.Attributes;

namespace PerfTester;

[MemoryDiagnoser(false)]
public class ArraySegmentationBenchmark
{
    private const int Offset = 1;
    private const int Count = 2;

    private static readonly string[] Words = { "The", "quick", "brown", "fox", "jumps", "over", "the", "lazy", "dog" };

    [Benchmark]
    public string[] Linq()
    {
        return Words.Skip(Offset).Take(Count).ToArray();
    }

    [Benchmark]
    public ArraySegment<string> Segment()
    {
        return new ArraySegment<string>(Words, Offset, Count);
    }
    
    [Benchmark]
    public Span<string> Span()
    {
        return Words.AsSpan().Slice(Offset, Count);
    }
}