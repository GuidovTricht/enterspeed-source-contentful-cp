using Newtonsoft.Json;

namespace Enterspeed.Source.Contentful.CP.Models;

public class ContentfulObjectField : ContentfulField
{
    public ContentfulObjectField(dynamic field)
    {
        var value = JsonConvert.DeserializeObject<ContentfulResource>(field.Value.ToString());

        Name = field.Name;
        Type = typeof(ContentfulResource);
        Value = value;
    }

    public ContentfulResource GetValue()
    {
        return (ContentfulResource)Value;
    }
}