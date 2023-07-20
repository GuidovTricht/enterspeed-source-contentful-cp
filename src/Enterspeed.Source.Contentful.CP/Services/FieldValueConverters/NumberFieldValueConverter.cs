using Contentful.Core.Models.Management;
using Enterspeed.Source.Contentful.CP.Models;
using Enterspeed.Source.Sdk.Api.Models.Properties;

namespace Enterspeed.Source.Contentful.CP.Services.FieldValueConverters;

public class NumberFieldValueConverter : IEnterspeedFieldValueConverter
{
    public bool IsConverter(ContentfulField field)
    {
        return field.Type == typeof(double) || field.Type == typeof(long);
    }

    public IEnterspeedProperty Convert(ContentfulField field, Locale locale)
    {
        var number = 0d;
        if(double.TryParse(field.Value.ToString(), out var n))
        {
            number = n;
        }

        return new NumberEnterspeedProperty(field.Name, number);
    }
}