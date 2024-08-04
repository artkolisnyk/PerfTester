namespace PerfTester.Mappers;

public sealed class Order
{
    public Customer Customer { get; set; }
    public OrderOrigin Origin { get; set; }
    public DateTime Created { get; set; }
    public OrderItem[] Items { get; set; }
}

public sealed class Customer
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public sealed class OrderItem
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public enum OrderOrigin
{
    Site,
    Phone,
    Bot
}