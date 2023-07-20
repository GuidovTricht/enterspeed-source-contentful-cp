using Contentful.Core.Models;
using Contentful.Core.Models.Management;

namespace Enterspeed.Source.Contentful.CP.Services;

public class EntityIdentityService : IEntityIdentityService
{
    public string GetId(Entry<dynamic> content)
    {
        return GetId(content.SystemProperties.Id, content.SystemProperties.Locale);
    }

    public string GetId(Asset asset)
    {
        return GetId(asset.SystemProperties.Id, asset.SystemProperties.Locale);
    }

    public string GetId(string id, Locale locale)
    {
        return GetId(id, locale.Code);
    }

    private static string GetId(string id, string locale)
    {
        return $"{id}-{locale}".ToLower();
    }
}