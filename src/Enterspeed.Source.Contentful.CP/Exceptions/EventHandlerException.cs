using System;

namespace Enterspeed.Source.Contentful.CP.Exceptions;

public class EventHandlerException : Exception
{
    public EventHandlerException(string message) : base(message)
    {
    }
}