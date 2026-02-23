namespace Shared.Contracts.Commands;

public class RefundPaymentCommand
{
    public Guid OrderId { get; set; }
}