using Contentful.Core;

namespace Enterspeed.Source.Contentful.CP.Services;

public interface IContentfulClientService
{
    ContentfulClient GetClient();
}