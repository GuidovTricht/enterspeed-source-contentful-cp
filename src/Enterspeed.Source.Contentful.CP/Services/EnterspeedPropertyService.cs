using Enterspeed.Source.Sdk.Api.Models.Properties;
using System.Collections.Generic;
using System.Linq;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;
using Enterspeed.Source.Contentful.CP.Services.FieldValueConverters;
using Enterspeed.Source.Contentful.CP.Factories;
using Enterspeed.Source.Contentful.CP.Models;

namespace Enterspeed.Source.Contentful.CP.Services;

public class EnterspeedPropertyService : IEnterspeedPropertyService
{
    private const string MetaData = "metaData";
    private const string Title = "title";
    private const string Description = "description";
    private const string File = "file";
    private readonly IEnumerable<IEnterspeedFieldValueConverter> _fieldValueConverters;
    private readonly IContentfulFieldFactory _contentfulFieldFactory;

    public EnterspeedPropertyService(IEnumerable<IEnterspeedFieldValueConverter> fieldValueConverters, IContentfulFieldFactory contentfulFieldFactory)
    {
        _fieldValueConverters = fieldValueConverters;
        _contentfulFieldFactory = contentfulFieldFactory;
    }

    public IDictionary<string, IEnterspeedProperty> GetProperties(Entry<dynamic> content, Locale locale)
    {
        var properties = new Dictionary<string, IEnterspeedProperty>();
        foreach (var field in content.Fields)
        {
            ContentfulField contentfulField = _contentfulFieldFactory.Create(field);

            var converter = _fieldValueConverters.FirstOrDefault(x => x.IsConverter(contentfulField));

            var value = converter?.Convert(contentfulField, locale);

            if (value == null)
            {
                continue;
            }

            properties.Add(value.Name, value);
        }

        properties.Add(MetaData, CreateMetaData(content));

        return properties;
    }

    public IDictionary<string, IEnterspeedProperty> GetProperties(Asset asset, Locale locale)
    {
        var properties = new Dictionary<string, IEnterspeedProperty>
        {
            { Title, new StringEnterspeedProperty(Title, asset.Title) },
            { Description, new StringEnterspeedProperty(Description, asset.Description) },
            { File, CreateFileData(asset.File) },
            { MetaData, CreateMetaData(asset) }
        };

        return properties;
    }

    private static IEnterspeedProperty CreateMetaData(Entry<dynamic> content)
    {
        var metaData = new Dictionary<string, IEnterspeedProperty>
        {
            ["locale"] = new StringEnterspeedProperty("locale", content.SystemProperties.Locale),
            ["type"] = new StringEnterspeedProperty("type", content.SystemProperties.Type),
            ["environment"] = new StringEnterspeedProperty("environment", content.SystemProperties.Environment.SystemProperties.Id),
            ["createDate"] = new StringEnterspeedProperty("createDate", content.SystemProperties.CreatedAt?.ToString("yyyy-MM-ddTHH:mm:ss")),
            ["updateDate"] = new StringEnterspeedProperty("updateDate", content.SystemProperties.UpdatedAt?.ToString("yyyy-MM-ddTHH:mm:ss"))
        };

        return new ObjectEnterspeedProperty(MetaData, metaData);
    }

    private static IEnterspeedProperty CreateFileData(File file)
    {
        var fileData = new Dictionary<string, IEnterspeedProperty>
        {
            ["fileName"] = new StringEnterspeedProperty("fileName", file.FileName),
            ["url"] = new StringEnterspeedProperty("url", file.Url),
            ["contentType"] = new StringEnterspeedProperty("contentType", file.ContentType),
            ["size"] = new NumberEnterspeedProperty("environment", file.Details.Size)
        };

        if (file.Details.Image is not null)
        {
            fileData.Add("width", new NumberEnterspeedProperty("width", file.Details.Image.Width));
            fileData.Add("height", new NumberEnterspeedProperty("height", file.Details.Image.Height));
        }

        return new ObjectEnterspeedProperty("file", fileData);
    }

    private static IEnterspeedProperty CreateMetaData(Asset content)
    {
        var metaData = new Dictionary<string, IEnterspeedProperty>
        {
            ["locale"] = new StringEnterspeedProperty("locale", content.SystemProperties.Locale),
            ["type"] = new StringEnterspeedProperty("type", content.SystemProperties.Type),
            ["environment"] = new StringEnterspeedProperty("environment", content.SystemProperties.Environment.SystemProperties.Id),
            ["createDate"] = new StringEnterspeedProperty("createDate", content.SystemProperties.CreatedAt?.ToString("yyyy-MM-ddTHH:mm:ss")),
            ["updateDate"] = new StringEnterspeedProperty("updateDate", content.SystemProperties.UpdatedAt?.ToString("yyyy-MM-ddTHH:mm:ss"))
        };

        return new ObjectEnterspeedProperty(MetaData, metaData);
    }
}