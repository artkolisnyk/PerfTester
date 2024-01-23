namespace PerfTester.Mappers;

public class OrderDto
{
    public CustomerDto Customer { get; set; }
    public OrderOriginDto Origin { get; set; }
    public DateTime Created { get; set; }
    public OrderItemDto[] Items { get; set; }
}

public class CustomerDto
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public enum OrderOriginDto
{
    Site,
    Phone,
    Bot
}

public class OrderItemDto
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}