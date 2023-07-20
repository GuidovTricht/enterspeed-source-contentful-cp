using System.Collections.Generic;
using Contentful.Core.Models.Management;
using Enterspeed.Source.Contentful.CP.Models;
using Enterspeed.Source.Sdk.Api.Models.Properties;

namespace Enterspeed.Source.Contentful.CP.Services.FieldValueConverters;

public class ObjectFieldValueConverter : IEnterspeedFieldValueConverter
{
    private readonly IEntityIdentityService _entityIdentityService;

    public ObjectFieldValueConverter(IEntityIdentityService entityIdentityService)
    {
        _entityIdentityService = entityIdentityService;
    }

    public bool IsConverter(ContentfulField field)
    {
        return field.Type == typeof(ContentfulResource);
    }

    public IEnterspeedProperty Convert(ContentfulField field, Locale locale)
    {
        if (field == null || locale == null)
            return null;
        var value = ((ContentfulObjectField)field).GetValue();
        if (value == null)
            return null;
        var properties = new Dictionary<string, IEnterspeedProperty>();
        if (value.SystemProperties?.Id != null)
            properties.Add("id", new StringEnterspeedProperty("id", _entityIdentityService.GetId(value.SystemProperties.Id, locale)));
        if (value.SystemProperties?.Type != null)
            properties.Add("type", new StringEnterspeedProperty("type", value.SystemProperties.Type));
        if (value.SystemProperties?.Type != null)
            properties.Add("linkType", new StringEnterspeedProperty("linkType", value.SystemProperties.LinkType));

        return new ObjectEnterspeedProperty(field.Name, properties);
    }
}