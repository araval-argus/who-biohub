using System.Text.Json;
using Microsoft.AspNetCore.Http;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.API.Http.Extensions;

public static class HttpRequestExtensions
{
    public static async Task<Either<T?, Errors>> TryParseBodyJson<T>(this HttpRequest request, CancellationToken cancellationToken)
    {
        return await request.TryParseBodyJson<T>((t) => { }, cancellationToken);
    }

    public static async Task<Either<T?, Errors>> TryParseBodyJson<T>(this HttpRequest request, Action<T?> then, CancellationToken cancellationToken)
    {
        try
        {
            T? t = await JsonSerializer.DeserializeAsync<T>(request.Body, cancellationToken: cancellationToken);
            then(t);

            return new(t);
        }
        catch (Exception e)
        {
            return new(new Errors(ErrorType.RequestParsing, $"Can not parse request body: {e.Message}"));
        }
    }

    public static async Task<Either<T, Errors>> ParseBodyJson<T>(this HttpRequest request, CancellationToken cancellationToken)
    {
        return await request.ParseBodyJson<T>((t) => { }, cancellationToken);
    }

    public static async Task<Either<T, Errors>> ParseBodyJson<T>(this HttpRequest request, Action<T> then, CancellationToken cancellationToken)
    {

        Either<T?, Errors> res = await request.TryParseBodyJson<T>(cancellationToken);
        if (res.IsRight)
            return new(res.Right);

        if (res.Left == null)
            return new(new Errors(ErrorType.RequestParsing, "Can not parse body"));

        T t = res.Left;
        then(t);
        return new(t);
    }
}