using Shared.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// gRPC client registration
builder.Services.AddGrpcClient<PaymentGrpc.PaymentGrpcClient>(o =>
{
    o.Address = new Uri("https://localhost:7124");
});

var app = builder.Build();

app.MapGet("/create-order", async (PaymentGrpc.PaymentGrpcClient paymentClient) =>
{
    var response = await paymentClient.ValidatePaymentAsync(
        new PaymentRequest
        {
            OrderId = Guid.NewGuid().ToString(),
            Amount = 500
        });

    return Results.Ok(response);
});

app.Run();
