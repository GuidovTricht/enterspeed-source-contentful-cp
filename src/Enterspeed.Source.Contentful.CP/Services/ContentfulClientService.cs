using Contentful.Core;
using System.Net.Http;

namespace Enterspeed.Source.Contentful.CP.Services;

public class ContentfulClientService : IContentfulClientService
{
    private readonly ContentfulClient _client;

    public ContentfulClientService(IEnterspeedConfigurationService enterspeedConfigurationService)
    {
        var configuration = enterspeedConfigurationService.GetConfiguration();
        var httpClient = new HttpClient();

        _client = new ContentfulClient(httpClient, configuration.ContentfulDeliveryApiKey, configuration.ContentfulPreviewApiKey, spaceId: configuration.ContentfulSpaceId);
    }

    public ContentfulClient GetClient()
    {
        return _client;
    }
}