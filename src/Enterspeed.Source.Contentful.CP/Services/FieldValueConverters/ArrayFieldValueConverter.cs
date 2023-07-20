using System;
using System.Collections.Generic;
using Contentful.Core.Models.Management;
using Enterspeed.Source.Contentful.CP.Models;
using Enterspeed.Source.Sdk.Api.Models.Properties;

namespace Enterspeed.Source.Contentful.CP.Services.FieldValueConverters;

public class ArrayFieldValueConverter : IEnterspeedFieldValueConverter
{
    private readonly IEntityIdentityService _entityIdentityService;

    public ArrayFieldValueConverter(IEntityIdentityService entityIdentityService)
    {
        _entityIdentityService = entityIdentityService;
    }

    public bool IsConverter(ContentfulField field)
    {
        return field.Type == typeof(string[]) || field.Type == typeof(ContentfulResource[]);
    }

    public IEnterspeedProperty Convert(ContentfulField field, Locale locale)
    {
        if (field.Type == typeof(string[]))
        {
            var value = ((ContentfulArrayField)field).GetValue<string[]>();

            var arrayItems = new List<IEnterspeedProperty>();
            foreach (var stringValue in value)
            {
                arrayItems.Add(new StringEnterspeedProperty(stringValue));
            }

            return new ArrayEnterspeedProperty(field.Name, arrayItems.ToArray());
        }
        if (field.Type == typeof(ContentfulResource[]))
        {
            var value = ((ContentfulArrayField)field).GetValue<ContentfulResource[]>();

            var arrayItems = new List<IEnterspeedProperty>();
            foreach (var contentfulResource in value)
            {
                arrayItems.Add(new ObjectEnterspeedProperty(new Dictionary<string, IEnterspeedProperty>
                {
                    ["id"] = new StringEnterspeedProperty("id", _entityIdentityService.GetId(contentfulResource.SystemProperties.Id, locale)),
                    ["type"] = new StringEnterspeedProperty("type", contentfulResource.SystemProperties.Type),
                    ["linkType"] = new StringEnterspeedProperty("linkType", contentfulResource.SystemProperties.LinkType)
                }));
            }

            return new ArrayEnterspeedProperty(field.Name, arrayItems.ToArray());
        }

        throw new ArgumentException($"Invalid argument for {nameof(ArrayFieldValueConverter)}");
    }
}