
using System.Text.Json;

namespace appSacoleiro.API.Middleware;

public sealed class ValidationExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentException ex)
        {
            await RetornarErro(context, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            await RetornarErro(context, ex.Message);
        }
    }

    private static async Task RetornarErro(
        HttpContext context,
        string mensagem)
    {
        context.Response.Clear();
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        context.Response.ContentType = "application/json; charset=utf-8";

        var resposta = new
        {
            mensagem
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(resposta));
    }
}
