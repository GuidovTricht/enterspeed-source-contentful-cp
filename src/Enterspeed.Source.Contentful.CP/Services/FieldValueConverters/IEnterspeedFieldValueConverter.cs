using Contentful.Core.Models.Management;
using Enterspeed.Source.Contentful.CP.Models;
using Enterspeed.Source.Sdk.Api.Models.Properties;

namespace Enterspeed.Source.Contentful.CP.Services.FieldValueConverters;

public interface IEnterspeedFieldValueConverter
{
    bool IsConverter(ContentfulField field);
    IEnterspeedProperty Convert(ContentfulField field, Locale locale);
}