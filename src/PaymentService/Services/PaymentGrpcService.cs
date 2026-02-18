using Shared.Contracts;
using Grpc.Core;

public class PaymentGrpcService : PaymentGrpc.PaymentGrpcBase
{
    public override Task<PaymentResponse> ValidatePayment(
        PaymentRequest request,
        ServerCallContext context)
    {
        var success = request.Amount < 1000; // basit kural

        return Task.FromResult(new PaymentResponse
        {
            IsSuccess = success,
            Message = success ? "Payment Approved" : "Payment Rejected"
        });
    }
}