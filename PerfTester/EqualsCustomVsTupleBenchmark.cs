using BenchmarkDotNet.Attributes;

namespace PerfTester;

[MemoryDiagnoser(false)]
public class EqualsCustomVsTupleBenchmark
{
    [Benchmark(Baseline = true)]
    public bool Custom()
    {
        PointCustomEquals p1 = new();
        PointCustomEquals p2 = new();

        return p1.Equals(p2);
    }

    [Benchmark]
    public bool Tuple()
    {
        PointTupleEquals p1 = new();
        PointTupleEquals p2 = new();

       return p1.Equals(p2);
    }

    [Benchmark]
    public bool ValueTuple()
    {
        PointValueTupleEquals p1 = new();
        PointValueTupleEquals p2 = new();

        return p1.Equals(p2);
    }
    
    [Benchmark]
    public bool Record()
    {
        PointRecordEquals p1 = new();
        PointRecordEquals p2 = new();

        return p1.Equals(p2);
    }

    public sealed class PointCustomEquals
    {
        private readonly int _x;
        private readonly int _y;

        public override bool Equals(object obj)
        { 
            PointCustomEquals p = (PointCustomEquals)obj;

            return _x == p._x && _y == p._y;
        }
    }

    public sealed class PointTupleEquals
    {
        private readonly int _x;
        private readonly int _y;

        public override bool Equals(object obj)
        {
            PointTupleEquals p = (PointTupleEquals)obj;

            return System.Tuple.Create(_x, _y).Equals(System.Tuple.Create(p._x, p._y));
        }
    }

    public sealed class PointValueTupleEquals
    {
        private readonly int _x;
        private readonly int _y;

        public override bool Equals(object obj)
        {
            PointValueTupleEquals p = (PointValueTupleEquals)obj;

            return (_x, _y).Equals((p._x, p._y));
        }
    }

    public sealed record PointRecordEquals(int X = 0, int Y = 0);
}