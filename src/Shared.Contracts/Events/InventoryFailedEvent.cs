namespace Shared.Contracts.Events;

public class InventoryFailedEvent
{
    public Guid OrderId { get; set; }
}