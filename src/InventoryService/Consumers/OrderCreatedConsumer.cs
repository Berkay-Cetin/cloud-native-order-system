using MassTransit;
using Shared.Contracts;
using Shared.Contracts.Events;

namespace InventoryService.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;

        Console.WriteLine("=== INVENTORY SERVICE ===");
        Console.WriteLine($"OrderId: {message.OrderId}");
        Console.WriteLine($"Product: {message.ProductName}");
        Console.WriteLine($"Price: {message.Price}");
        Console.WriteLine("Stock düşüldü.");
    }
}
