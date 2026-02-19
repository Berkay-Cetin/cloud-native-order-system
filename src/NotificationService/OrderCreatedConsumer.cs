using MassTransit;
using Shared.Contracts.Events;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        Console.WriteLine($"Order received: {context.Message.OrderId}");
        return Task.CompletedTask;
    }
}