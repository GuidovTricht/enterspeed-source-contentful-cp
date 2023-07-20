using System.Collections.Generic;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;
using Enterspeed.Source.Contentful.CP.Constants;
using Enterspeed.Source.Contentful.CP.Services;
using Enterspeed.Source.Sdk.Api.Models;
using Enterspeed.Source.Sdk.Api.Models.Properties;

namespace Enterspeed.Source.Contentful.CP.Models;

public class EnterspeedEntity : IEnterspeedEntity
{
    public EnterspeedEntity(Entry<dynamic> entry, Locale locale, IEnterspeedPropertyService enterspeedPropertyService, IEntityIdentityService entityIdentityService)
    {
        Id = entityIdentityService.GetId(entry);
        Type = entry.SystemProperties.ContentType.SystemProperties.Id;
        Properties = enterspeedPropertyService.GetProperties(entry, locale);
    }

    public EnterspeedEntity(Asset asset, Locale locale, IEnterspeedPropertyService enterspeedPropertyService, IEntityIdentityService entityIdentityService)
    {
        Id = entityIdentityService.GetId(asset);
        Type = WebhooksConstants.Types.Asset.ToLower();
        Properties = enterspeedPropertyService.GetProperties(asset, locale);
    }

    public string Id { get; }
    public string Type { get; }
    public string Url => null;
    public string[] Redirects => null;
    public string ParentId => null;
    public IDictionary<string, IEnterspeedProperty> Properties { get; }
}