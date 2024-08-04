using AutoMapper;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using Mapster;
using TMapper = Nelibur.ObjectMapper.TinyMapper;
using EMapper = ExpressMapper.Mapper;
using AMapper = AgileObjects.AgileMapper.Mapper;

namespace PerfTester.Mappers;

//[SimpleJob(RunStrategy.Throughput)]
[MemoryDiagnoser]
public class MappersBenchmark
{
    private Order _order;
    private IMapper _autoMapper;

    [GlobalSetup]
    public void Setup()
    {
        _order = new()
        {
            Customer = new()
            {
                Name = "John Smith",
                Email = "johnsmith@mail.com"
            },
            Origin = OrderOrigin.Site,
            Created = new(2023, 1, 23),
            Items =
            [
                new()
                {
                    Name = "Snickers",
                    Quantity = 1,
                    Price = 99.9m
                },
                new()
                {
                    Name = "T-Shirt",
                    Quantity = 2,
                    Price = 29.9m
                }
            ]
        };
        
        // Automapper config
        MapperConfiguration config = new(cfg =>
        {
            cfg.CreateMap<Order, OrderDto>();
            cfg.CreateMap<Customer, CustomerDto>();
            cfg.CreateMap<OrderOrigin, OrderOriginDto>();
            cfg.CreateMap<OrderItem, OrderItemDto>();
        });

        _autoMapper = config.CreateMapper();
        
        // TinyMapper config
        TMapper.Bind<Order, OrderDto>();
        TMapper.Bind<Customer, CustomerDto>();
        TMapper.Bind<OrderOrigin, OrderOriginDto>();
        TMapper.Bind<OrderItem, OrderItemDto>();
        
        // ExpressMapper config
        EMapper.Register<Order, OrderDto>();
        EMapper.Register<Customer, CustomerDto>();
        EMapper.Register<OrderOrigin, OrderOriginDto>();
        EMapper.Register<OrderItem, OrderItemDto>();
    }

    [Benchmark]
    public OrderDto AutoMapper()
    {
        return _autoMapper.Map<OrderDto>(_order);
    }
    
    [Benchmark]
    public OrderDto TinyMapper()
    {
        return TMapper.Map<OrderDto>(_order);
    }
    
    [Benchmark]
    public OrderDto ExpressMapper()
    {
        return EMapper.Map<Order, OrderDto>(_order);
    }
    
    [Benchmark]
    public OrderDto AgileMapper()
    {
        // version 1.8.0 is used because it's built in Release mode
        return AMapper.Map(_order).ToANew<OrderDto>();
    }
    
    [Benchmark]
    public OrderDto Mapster()
    {
        return _order.Adapt<OrderDto>();
    }
    
    [Benchmark]
    public OrderDto Mapperly()
    {
        return MapperlyMapper.Map(_order);
    }
    
    [Benchmark(Baseline = true)]
    public OrderDto Manual()
    {
        return ManualMapper.Map(_order);
    }
}