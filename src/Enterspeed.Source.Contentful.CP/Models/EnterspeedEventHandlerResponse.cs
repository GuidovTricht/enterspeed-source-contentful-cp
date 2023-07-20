using Enterspeed.Source.Sdk.Domain.Connection;

namespace Enterspeed.Source.Contentful.CP.Models;

public class EnterspeedEventHandlerResponse
{
    public string Message { get; }
    public Response Response { get; }

    public EnterspeedEventHandlerResponse(string message, Response response)
    {
        Message = message;
        Response = response;
    }
}