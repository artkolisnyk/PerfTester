namespace PerfTester.Mappers;

public class Order
{
    public Customer Customer { get; set; }
    public OrderOrigin Origin { get; set; }
    public DateTime Created { get; set; }
    public OrderItem[] Items { get; set; }
}

public class Customer
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public enum OrderOrigin
{
    Site,
    Phone,
    Bot
}

public class OrderItem
{
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}