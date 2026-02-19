namespace Shared.Contracts.Events;

public class OrderCreatedEvent
{
    public Guid OrderId { get; set; }
    public string ProductName { get; set; } = default!;
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
}