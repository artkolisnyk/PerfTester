using BenchmarkDotNet.Attributes;

namespace PerfTester;

[MemoryDiagnoser(false)]
public class MaskStringBenchmark
{
    private const string ccn = "4411332255779900";

    [Benchmark]
    public string Span()
    {
        Span<char> chars = stackalloc char[ccn.Length];
    
        ccn.TryCopyTo(chars);
    
        for (int i = 4; i < chars.Length - 4; i++)
        {
            chars[i] = '*';
        }
    
        return new(chars);
    }

    [Benchmark]
    public string StringCreate()
    {
        return string.Create(ccn.Length, ccn, (span, s) => CreateString(s, span));
    }

    [Benchmark(Baseline = true)]
    public string CharArray()
    {
        var chars = ccn.ToCharArray();
        
        for (int i = 4; i < chars.Length - 4; i++)
        {
            chars[i] = '*';
        }

        return new string(chars);
    }

    [Benchmark]
    public unsafe string UnsafePointers()
    {
        var copy = new string(ccn);
    
        fixed (char* c = copy)
        {
            for (int i = 4; i < copy.Length - 4; i++)
            {
                c[i] = '*';
            }
        }
    
        return copy;
    }
    
    private static void CreateString(string s, Span<char> span)
    {
        for (int i = 0; i < s.Length; i++)
        {
            if (i >= 4 && i < s.Length - 4)
            {
                span[i] = '*';
                continue;
            }

            span[i] = s[i];
        }
    }
}