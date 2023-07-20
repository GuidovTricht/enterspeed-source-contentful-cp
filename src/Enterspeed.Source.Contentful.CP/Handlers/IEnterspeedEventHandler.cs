using Contentful.Core.Models;

namespace Enterspeed.Source.Contentful.CP.Handlers;

public interface IEnterspeedEventHandler
{
    bool CanHandle(IContentfulResource resource, string eventType);
    void Handle(IContentfulResource resource);
}