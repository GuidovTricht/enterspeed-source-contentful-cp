using System.Net;
using Contentful.Core.Models;
using Enterspeed.Source.Contentful.CP.Constants;
using Enterspeed.Source.Contentful.CP.Exceptions;
using Enterspeed.Source.Contentful.CP.Services;
using Enterspeed.Source.Sdk.Api.Services;

namespace Enterspeed.Source.Contentful.CP.Handlers;

public class EntryDeleteEventHandler : IEnterspeedEventHandler
{
    private readonly IEnterspeedIngestService _enterspeedIngestService;
    private readonly IEntityIdentityService _entityIdentityService;
    private readonly IContentfulClientService _contentfulClientService;

    public EntryDeleteEventHandler(IEnterspeedIngestService enterspeedIngestService, IEntityIdentityService entityIdentityService, IContentfulClientService contentfulClientService)
    {
        _enterspeedIngestService = enterspeedIngestService;
        _entityIdentityService = entityIdentityService;
        _contentfulClientService = contentfulClientService;
    }
    public bool CanHandle(IContentfulResource resource, string eventType)
    {
        return resource?.SystemProperties?.Type == WebhooksConstants.Types.DeletedEntry
               && !string.IsNullOrWhiteSpace(resource?.SystemProperties?.Id)
               && (eventType == WebhooksConstants.Events.EntryDelete || eventType == WebhooksConstants.Events.EntryUnpublish);
    }

    public async void Handle(IContentfulResource resource)
    {
        var client = _contentfulClientService.GetClient();

        var locales = await client.GetLocales();

        foreach (var locale in locales)
        {
            var id = _entityIdentityService.GetId(resource.SystemProperties.Id, locale);

            var deleteResponse = _enterspeedIngestService.Delete(id);
            if (!deleteResponse.Success && deleteResponse.Status != HttpStatusCode.NotFound)
            {
                throw new EventHandlerException($"Failed deleting entity ({id}). Message: {deleteResponse.Message}");
            }
        }
    }
}