using MassTransit;
using Shared.Contracts;
using Shared.Contracts.Events;

var builder = WebApplication.CreateBuilder(args);

// gRPC server (ileride lazım olabilir)
// builder.Services.AddGrpc();

// --------------------
// MassTransit + RabbitMQ
// --------------------
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});

// --------------------
// gRPC Client (PaymentService)
// --------------------
builder.Services.AddGrpcClient<PaymentGrpc.PaymentGrpcClient>(o =>
{
    o.Address = new Uri("http://localhost:5006");
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(5030, o =>
    {
        o.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1;
    });
});


var app = builder.Build();

// --------------------
// Minimal API Endpoint
// --------------------
app.MapGet("/create-order", async (
    PaymentGrpc.PaymentGrpcClient paymentClient,
    IPublishEndpoint publishEndpoint) =>
{
    var orderId = Guid.NewGuid();

    var paymentResponse = await paymentClient.ValidatePaymentAsync(
        new PaymentRequest
        {
            OrderId = orderId.ToString(),
            Amount = 500
        });

    if (paymentResponse.IsSuccess)
    {
        await publishEndpoint.Publish(new OrderCreatedEvent
        {
            OrderId = orderId,
            ProductName = "Sample Product",
            Price = 500,
            CreatedAt = DateTime.UtcNow
        });
    }

    return Results.Ok(paymentResponse);
});

app.Run();
