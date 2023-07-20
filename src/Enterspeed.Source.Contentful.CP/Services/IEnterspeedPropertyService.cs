using System.Collections.Generic;
using Contentful.Core.Models;
using Contentful.Core.Models.Management;
using Enterspeed.Source.Sdk.Api.Models.Properties;

namespace Enterspeed.Source.Contentful.CP.Services;

public interface IEnterspeedPropertyService
{
    IDictionary<string, IEnterspeedProperty> GetProperties(Entry<dynamic> content, Locale locale);
    IDictionary<string, IEnterspeedProperty> GetProperties(Asset asset, Locale locale);
}