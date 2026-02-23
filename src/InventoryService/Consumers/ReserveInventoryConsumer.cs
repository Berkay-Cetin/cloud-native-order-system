using MassTransit;
using Shared.Contracts.Commands;
using Shared.Contracts.Events;

namespace InventoryService.Consumers;

public class ReserveInventoryConsumer : IConsumer<ReserveInventoryCommand>
{
    private static readonly Random _random = new();

    public async Task Consume(ConsumeContext<ReserveInventoryCommand> context)
    {
        var orderId = context.Message.OrderId;
        var success = _random.Next(0, 2) == 0;

        Console.WriteLine($"[Inventory] Processing {orderId}");

        await Task.Delay(500);

        if (success)
        {
            Console.WriteLine($"[Inventory] SUCCESS {orderId}");

            await context.Publish(new InventoryReservedEvent
            {
                OrderId = orderId
            });
        }
        else
        {
            Console.WriteLine($"[Inventory] FAILED {orderId}");

            await context.Publish(new InventoryFailedEvent
            {
                OrderId = orderId
            });
        }
    }
}