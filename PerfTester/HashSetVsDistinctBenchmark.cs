using BenchmarkDotNet.Attributes;

namespace PerfTester;

[MemoryDiagnoser(false)]
public class HashSetVsDistinctBenchmark
{
    private const string Symbols = "!abcdefghijklmnopqrstuvwxyz1234567890?, ;:ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private string[] _data;

    [Params(1000, 1_000_000)]
    public int Size { get; set; }
    
    [GlobalSetup]
    public void Setup()
    {
        _data = new string[Size];

        for (int i = 0; i < _data.Length; i++)
        {
            _data[i] = Symbols[Random.Shared.Next(Symbols.Length)].ToString();
        }
    }
    
    [Benchmark(Baseline = true)]
    public HashSet<string> HashSetViaCtor()
    {
        return new(_data);
    }

    [Benchmark]
    public HashSet<string> HashSetCollectionExpression()
    {
        return [.._data];
    }
    
    [Benchmark]
    public string[] DistinctToArray()
    {
        return _data.Distinct().ToArray();
    }
}