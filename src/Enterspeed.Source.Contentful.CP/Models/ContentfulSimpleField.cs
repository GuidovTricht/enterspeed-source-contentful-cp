namespace Enterspeed.Source.Contentful.CP.Models;

public class ContentfulSimpleField : ContentfulField
{
    public ContentfulSimpleField(dynamic field)
    {
        Name = field.Name;
        Type = field.Value.Value.GetType();
        Value = field.Value.Value;
    }
}