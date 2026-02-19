using MassTransit;
using Shared.Contracts;
using Shared.Contracts.Events;

namespace NotificationService.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        var message = context.Message;

        Console.WriteLine("=== NOTIFICATION SERVICE ===");
        Console.WriteLine($"OrderId: {message.OrderId}");
        Console.WriteLine("Kullanıcıya bildirim gönderildi.");
    }
}
