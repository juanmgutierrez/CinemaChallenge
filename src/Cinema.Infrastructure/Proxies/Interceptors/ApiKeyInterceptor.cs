using Grpc.Core;
using Grpc.Core.Interceptors;

namespace Cinema.Infrastructure.Proxies.Interceptors;

internal class ApiKeyInterceptor : Interceptor
{
    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        //var client = new MoviesApi.MoviesApiClient(channel);
        //// TODO Secure the API Key
        //var headers = new Grpc.Core.Metadata { { "X-Apikey", "68e5fbda-9ec9-4858-97b2-4a8349764c63" } };
        //// TODO Add Deadline and Cancellation Token
        //var getAllReply = await client.GetAllAsync(new Empty(), headers);

        //context.Options.Headers.Add("X-Apikey", "68e5fbda-9ec9-4858-97b2-4a8349764c63");

        //return continuation(request, context);

        var headerEntry = new Metadata.Entry("X-Apikey", "68e5fbda-9ec9-4858-97b2-4a8349764c63");

        if (context.Options.Headers is null)
            context.Options.WithHeaders(new Metadata() { headerEntry });
        else
            context.Options.Headers.Add(headerEntry);

        return base.AsyncUnaryCall(request, context, continuation);
    }
}
