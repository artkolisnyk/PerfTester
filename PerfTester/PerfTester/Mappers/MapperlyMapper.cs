using Riok.Mapperly.Abstractions;

namespace PerfTester.Mappers;

[Mapper]
public static partial class MapperlyMapper
{
    public static partial OrderDto Map(Order order);
}