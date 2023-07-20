using System.Collections.Generic;
using System.Linq;
using Contentful.Core;
using Contentful.Core.Models;
using Contentful.Core.Search;
using Enterspeed.Source.Contentful.CP.Constants;
using Enterspeed.Source.Contentful.CP.Models;
using Enterspeed.Source.Contentful.CP.Services;

namespace Enterspeed.Source.Contentful.CP.Handlers;

public class SeedEventHandler : IEnterspeedEventHandler
{
    private readonly IContentfulClientService _contentfulClientService;
    private readonly IEnumerable<IEnterspeedEventHandler> _enterspeedEventHandlers;

    public SeedEventHandler(IContentfulClientService contentfulClientService, IEnumerable<IEnterspeedEventHandler> enterspeedEventHandlers)
    {
        _contentfulClientService = contentfulClientService;
        _enterspeedEventHandlers = enterspeedEventHandlers;
    }

    public bool CanHandle(IContentfulResource resource, string eventType)
    {
        return eventType == WebhooksConstants.Events.Seed;
    }

    public void Handle(IContentfulResource resource)
    {
        var client = _contentfulClientService.GetClient();

        HandleEntries(client);
        HandleAssets(client);
    }

    private async void HandleEntries(ContentfulClient client)
    {
        var entryQueryBuilder = QueryBuilder<ContentfulResource>.New;
        var entries = await client.GetEntries(entryQueryBuilder);

        var entryPublishEventHandler = _enterspeedEventHandlers.First(x => x.GetType() == typeof(EntryPublishEventHandler));

        foreach (var entry in entries)
        {
            entryPublishEventHandler.Handle(entry);
        }
    }

    private async void HandleAssets(ContentfulClient client)
    {
        var assetQueryBuilder = QueryBuilder<Asset>.New;
        var assets = await client.GetAssets(assetQueryBuilder);

        var assetPublishEventHandler = _enterspeedEventHandlers.First(x => x.GetType() == typeof(AssetPublishEventHandler));

        foreach (var asset in assets)
        {
            assetPublishEventHandler.Handle(asset);
        }
    }
}