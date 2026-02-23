namespace Shared.Contracts.Events;

public class PaymentFailedEvent
{
    public Guid OrderId { get; set; }
}