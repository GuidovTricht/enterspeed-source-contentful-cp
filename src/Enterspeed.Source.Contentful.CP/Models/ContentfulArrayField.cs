using System;
using Newtonsoft.Json;

namespace Enterspeed.Source.Contentful.CP.Models;

public class ContentfulArrayField : ContentfulField
{
    public ContentfulArrayField(dynamic field)
    {
        Name = field.Name;

        // Not very nice way to determine the type of the array, can we do this better?
        try
        {
            Value = JsonConvert.DeserializeObject<ContentfulResource[]>(field.Value.ToString());
            Type = typeof(ContentfulResource[]);
            return;
        }
        catch { /*ignored*/ }

        try
        {
            Value = JsonConvert.DeserializeObject<string[]>(field.Value.ToString());
            Type = typeof(string[]);
            return;
        }
        catch { /*ignored*/ }

        throw new ArgumentException("Array value is not of any supported generic type.");
    }
}