namespace OrderService.Sagas;

using MassTransit;
using Shared.Contracts.Commands;
using Shared.Contracts.Events;

public class OrderStateMachine : MassTransitStateMachine<OrderState>
{
    public State PaymentPending { get; private set; }
    public State InventoryPending { get; private set; }

    public Event<PaymentCompletedEvent> PaymentCompleted { get; private set; }
    public Event<PaymentFailedEvent> PaymentFailed { get; private set; }
    public Event<InventoryReservedEvent> InventoryReserved { get; private set; }
    public Event<InventoryFailedEvent> InventoryFailed { get; private set; }

    public OrderStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => PaymentCompleted,
            x => x.CorrelateById(m => m.Message.OrderId));

        Event(() => PaymentFailed,
            x => x.CorrelateById(m => m.Message.OrderId));

        Event(() => InventoryReserved,
            x => x.CorrelateById(m => m.Message.OrderId));

        Event(() => InventoryFailed,
            x => x.CorrelateById(m => m.Message.OrderId));

        Initially(
        When(PaymentCompleted)
            .Then(context =>
            {
                Console.WriteLine($"[Saga] Payment completed for {context.Instance.CorrelationId}");
            })
            .TransitionTo(InventoryPending)
            .Publish(context => new ReserveInventoryCommand
            {
                OrderId = context.Instance.CorrelationId
            })
        );

        During(InventoryPending,
        When(InventoryReserved)
            .Then(context =>
            {
                Console.WriteLine($"[Saga] Inventory reserved for {context.Instance.CorrelationId}");
                Console.WriteLine($"[Saga] ORDER COMPLETED");
            })
            .Finalize(),

        When(InventoryFailed)
            .Then(context =>
            {
                Console.WriteLine($"[Saga] Inventory FAILED for {context.Instance.CorrelationId}");
                Console.WriteLine($"[Saga] Triggering Refund");
            })
            .Publish(context => new RefundPaymentCommand
            {
                OrderId = context.Instance.CorrelationId
            })
            .Finalize()
        );

        SetCompletedWhenFinalized();
    }
}