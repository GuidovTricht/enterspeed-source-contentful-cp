using Contentful.Core.Models;
using Contentful.Core.Search;
using Enterspeed.Source.Contentful.CP.Constants;
using Enterspeed.Source.Contentful.CP.Exceptions;
using Enterspeed.Source.Contentful.CP.Models;
using Enterspeed.Source.Contentful.CP.Services;
using Enterspeed.Source.Sdk.Api.Services;

namespace Enterspeed.Source.Contentful.CP.Handlers;

public class AssetPublishEventHandler : IEnterspeedEventHandler
{
    private readonly IContentfulClientService _contentfulClientService;
    private readonly IEnterspeedPropertyService _enterspeedPropertyService;
    private readonly IEntityIdentityService _entityIdentityService;
    private readonly IEnterspeedIngestService _enterspeedIngestService;

    public AssetPublishEventHandler(IContentfulClientService contentfulClientService, IEnterspeedPropertyService enterspeedPropertyService, IEntityIdentityService entityIdentityService, IEnterspeedIngestService enterspeedIngestService)
    {
        _contentfulClientService = contentfulClientService;
        _enterspeedPropertyService = enterspeedPropertyService;
        _entityIdentityService = entityIdentityService;
        _enterspeedIngestService = enterspeedIngestService;
    }

    public bool CanHandle(IContentfulResource resource, string eventType)
    {
        return resource?.SystemProperties?.Type == WebhooksConstants.Types.Asset
               && !string.IsNullOrWhiteSpace(resource?.SystemProperties?.Id) 
               && eventType == WebhooksConstants.Events.AssetPublish;
    }

    public async void Handle(IContentfulResource resource)
    {
        var client = _contentfulClientService.GetClient();
        var locales = await client.GetLocales();

        foreach (var locale in locales)
        {
            var queryBuilder = QueryBuilder<Asset>.New.LocaleIs(locale.Code);
            var asset = await client.GetAsset(resource.SystemProperties.Id, queryBuilder);

            var entity = new EnterspeedEntity(asset, locale, _enterspeedPropertyService, _entityIdentityService);

            var saveResponse = _enterspeedIngestService.Save(entity);
            if (!saveResponse.Success)
            {
                var message = saveResponse.Exception != null
                    ? saveResponse.Exception.Message
                    : saveResponse.Message;
                throw new EventHandlerException($"Failed ingesting entity ({entity.Id}). Message: {message}");
            }
        }
    }
}