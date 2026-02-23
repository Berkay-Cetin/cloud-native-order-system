using MassTransit;
using Shared.Contracts.Commands;

namespace PaymentService.Consumers;

public class RefundPaymentConsumer : IConsumer<RefundPaymentCommand>
{
    public Task Consume(ConsumeContext<RefundPaymentCommand> context)
    {
        Console.WriteLine($"[Payment] Refunding payment for {context.Message.OrderId}");

        return Task.CompletedTask;
    }
}