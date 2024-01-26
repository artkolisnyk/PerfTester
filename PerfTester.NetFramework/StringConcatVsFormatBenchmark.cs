using BenchmarkDotNet.Attributes;

namespace PerfTester.NetFramework
{
    [MemoryDiagnoser(false)]
    public class StringConcatVsFormatBenchmark
    {
        private const string Name = "Vova";
        private const int Age = 20;
        private const string Format = "00";

        [Benchmark(Baseline = true)]
        public string ConcatSimple()
        {
            return "My name is " + Name + ". I'm " + Age + " years old.";
        }

        [Benchmark]
        public string ConcatCustom()
        {
            return "My name is " + Name + ". I'm " + Age.ToString(Format) + " years old.";
        }

        [Benchmark]
        public string ConcatLongerThan360()
        {
            return
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa. My name is " +
                Name +
                ". I'm " +
                Age +
                " years old.";
        }

        [Benchmark]
        public string FormatSimple()
        {
            return string.Format("My name is {0}. I'm {1} years old.", Name, Age);
        }

        [Benchmark]
        public string FormatCustomBoxed()
        {
            return string.Format("My name is {0}. I'm {1:Format} years old.", Name, Age);
        }

        [Benchmark]
        public string FormatCustomViaString()
        {
            return string.Format("My name is {0}. I'm {1} years old.", Name, Age.ToString(Format));
        }

        [Benchmark]
        public string FormatLongerThan360()
        {
            return string.Format(
                "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa. My name is {0}. I'm {1:Format} years old.",
                Name, Age);
        }
    }
}