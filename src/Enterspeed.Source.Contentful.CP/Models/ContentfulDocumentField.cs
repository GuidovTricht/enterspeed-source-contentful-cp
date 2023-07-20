using Contentful.Core.Configuration;
using Contentful.Core.Models;
using Newtonsoft.Json;

namespace Enterspeed.Source.Contentful.CP.Models;

public class ContentfulDocumentField : ContentfulField
{
    public ContentfulDocumentField(dynamic field)
    {
        var serializerSettings = new JsonSerializerSettings();
        serializerSettings.Converters.Add(new ContentJsonConverter());
        var document = JsonConvert.DeserializeObject<Document>(field.Value.ToString(), serializerSettings);

        Name = field.Name;
        Type = typeof(string);

        var htmlRenderer = new HtmlRenderer();
        Value = htmlRenderer.ToHtml(document).GetAwaiter().GetResult();
    }
}