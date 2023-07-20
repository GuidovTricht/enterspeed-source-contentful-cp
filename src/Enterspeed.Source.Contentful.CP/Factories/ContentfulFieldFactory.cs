using Enterspeed.Source.Contentful.CP.Models;
using Newtonsoft.Json.Linq;

namespace Enterspeed.Source.Contentful.CP.Factories;

public class ContentfulFieldFactory : IContentfulFieldFactory
{
    public ContentfulField Create(dynamic field)
    {
        if (field.Value.GetType() == typeof(JObject))
        {
            if (!string.IsNullOrEmpty(field.Value["nodeType"]?.Value))
                return new ContentfulDocumentField(field);
            return new ContentfulObjectField(field);
        }
        
        if (field.Value.GetType() == typeof(JArray))
            return new ContentfulArrayField(field);
        
        return new ContentfulSimpleField(field);
    }
}

public interface IContentfulFieldFactory
{
    ContentfulField Create(dynamic field);
}