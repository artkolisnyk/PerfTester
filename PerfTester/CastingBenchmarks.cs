using BenchmarkDotNet.Attributes;

namespace PerfTester;

public class CastingSimpleObjectsBenchmark
{
    private static readonly object Person;

    static CastingSimpleObjectsBenchmark()
    {
        Person = new Person { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() };
    }

    [Benchmark(Baseline = true)]
    public Person HardCast()
    {
        return (Person)Person;
    }
    
    [Benchmark]
    public Person SafeCast()
    {
        return Person as Person;
    }
    
    [Benchmark]
    public Person MatchCast()
    {
        if (Person is Person person)
        {
            return person;
        }

        return null;
    }
}

[MemoryDiagnoser(false)]
public class CastingCollectionObjectsBenchmark
{
    private static readonly object[] People;

    static CastingCollectionObjectsBenchmark()
    {
        People = Enumerable.Range(0, 100)
            .Select(x => new Person { Id = Guid.NewGuid(), Name = Guid.NewGuid().ToString() }).ToArray();
    }

    [Benchmark(Baseline = true)]
    public Person[] OfType()
    {
        return People.OfType<Person>().ToArray();
    }
    
    [Benchmark]
    public Person[] Cast_As()
    {
        return People.Where(x => x as Person is not null).Cast<Person>().ToArray();
    }
    
    [Benchmark]
    public Person[] Cast_Is()
    {
        return People.Where(x => x is Person).Cast<Person>().ToArray();
    }
    
    [Benchmark]
    public Person[] HardCast_As()
    {
        return People.Where(x => x as Person is not null).Select(x => (Person)x).ToArray();
    }
    
    [Benchmark]
    public Person[] HardCast_Is()
    {
        return People.Where(x => x is Person).Select(x => (Person)x).ToArray();
    }
    
    [Benchmark]
    public Person[] HardCast_TypeOf()
    {
        return People.Where(x => x.GetType() == typeof(Person)).Select(x => (Person)x).ToArray();
    }
}

public class Person
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}