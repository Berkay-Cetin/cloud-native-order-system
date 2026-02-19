using MassTransit;
using Shared.Contracts.Events;

namespace PaymentService.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;

        Console.WriteLine("Order Created Event Received");
        Console.WriteLine($"OrderId: {message.OrderId}");
        Console.WriteLine($"Price: {message.Price}");
        Console.WriteLine($"CreatedAt: {message.CreatedAt}");

        return Task.CompletedTask;
    }
}
