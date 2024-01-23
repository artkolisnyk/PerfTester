namespace PerfTester.Mappers;

public static class ManualMapper
{
    public static OrderDto Map(Order order)
    {
        OrderDto dto = new()
        {
            Customer = new()
            {
                Name = order.Customer.Name,
                Email = order.Customer.Email
            },
            Origin = (OrderOriginDto)order.Origin,
            Created = order.Created,
            Items = order.Items
                .Select(i => new OrderItemDto
                {
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Price = i.Price
                })
                .ToArray()
        };

        return dto;
    }
}