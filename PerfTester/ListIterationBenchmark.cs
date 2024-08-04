using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace PerfTester;

[MemoryDiagnoser(false)]
public class ListIterationBenchmark
{
    private List<int> _items;
    
    [Params(1000, 1_000_000)]
    public int Size { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _items = Enumerable.Range(0, Size).Select(x => Random.Shared.Next()).ToList();
    }

    [Benchmark(Baseline = true)]
    public void For()
    {
        for (var i = 0; i < _items.Count; i++)
        {
            var item = _items[i];
        }
    }

    [Benchmark]
    public void ForEach()
    {
        foreach (var item in _items)
        {
        }
    }

    [Benchmark]
    public void ForEach_Linq()
    {
        _items.ForEach(x => { });
    }
    
    [Benchmark]
    public void Parallel_ForEach()
    {
        Parallel.ForEach(_items, x => { });
    }
    
    [Benchmark]
    public void Parallel_Linq()
    {
        _items.AsParallel().ForAll(x => { });
    }

    [Benchmark]
    public void ForEach_Span()
    {
        foreach (var i in CollectionsMarshal.AsSpan(_items))
        {
        }
    }
    
    [Benchmark]
    public void For_UnsafeSpan()
    {
        var span = CollectionsMarshal.AsSpan(_items);
        ref var searchSpace = ref MemoryMarshal.GetReference(span);

        for (var i = 0; i < span.Length; i++)
        {
            var item = Unsafe.Add(ref searchSpace, i);
        }
    }
}