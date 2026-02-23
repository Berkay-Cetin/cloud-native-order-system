namespace Shared.Contracts.Events;

public class PaymentCompletedEvent
{
    public Guid OrderId { get; set; }
}