using System.Net.Http;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using Enterspeed.Source.Contentful.CP.Constants;
using Enterspeed.Source.Contentful.CP.Exceptions;
using Enterspeed.Source.Contentful.CP.Models;
using Enterspeed.Source.Contentful.CP.Services;
using Enterspeed.Source.Sdk.Api.Services;

namespace Enterspeed.Source.Contentful.CP.Handlers;

public class EntryPublishEventHandler : IEnterspeedEventHandler
{
    private readonly IContentfulClientService _contentfulClientService;
    private readonly IEnterspeedPropertyService _enterspeedPropertyService;
    private readonly IEntityIdentityService _entityIdentityService;
    private readonly IEnterspeedIngestService _enterspeedIngestService;

    public EntryPublishEventHandler(IContentfulClientService contentfulClientService, IEnterspeedPropertyService enterspeedPropertyService, IEntityIdentityService entityIdentityService, IEnterspeedIngestService enterspeedIngestService)
    {
        _contentfulClientService = contentfulClientService;
        _enterspeedPropertyService = enterspeedPropertyService;
        _entityIdentityService = entityIdentityService;
        _enterspeedIngestService = enterspeedIngestService;
    }

    public bool CanHandle(IContentfulResource resource, string eventType)
    {
        return resource?.SystemProperties?.Type == WebhooksConstants.Types.Entry
               && !string.IsNullOrWhiteSpace(resource?.SystemProperties?.Id)
               && eventType == WebhooksConstants.Events.EntryPublish;
    }

    public async void Handle(IContentfulResource resource)
    {
        var client = _contentfulClientService.GetClient();

        var locales = await client.GetLocales();

        foreach (var locale in locales)
        {
            var queryBuilder = QueryBuilder<Entry<dynamic>>.New.LocaleIs(locale.Code);
            var entry = await client.GetEntry(resource.SystemProperties.Id, queryBuilder);
            var typedEntry = await client.GetEntry<HeroBanner>(resource.SystemProperties.Id);

            var entity = new EnterspeedEntity(entry, locale, _enterspeedPropertyService, _entityIdentityService);

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

public class HeroBanner : ContentfulResource
{
    public string Headline { get; set; }
    public Document Description { get; set; }
}