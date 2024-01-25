using BenchmarkDotNet.Attributes;

namespace PerfTester;

[MemoryDiagnoser(false)]
public class PatternMatchingBenchmark
{
    private const string Text = "HELLO THERE. MY NAME IS C# AND I'M HERE TO TELL YOU ABOUT PATTERN MATCHING PERFORMANCE.";

    [Benchmark]
    public bool IsLetterPattern()
    {
        return Text.All(c => c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z'));
    }

    [Benchmark]
    public bool IsLetterNormal()
    {
        return Text.All(c => (c >= 'a' && c <= 'z') || (c >= 'A' && c >= 'Z'));
    }
    
    [Benchmark(Baseline = true)]
    public bool IsLetterEarlyReturn()
    {
        return Text.All(c =>
        {
            if (c < 'a')
                return false;

            if (c <= 'z')
                return true;

            if (c < 'A')
                return false;

            if (c <= 'Z')
                return true;

            return false;
        });
    }
}