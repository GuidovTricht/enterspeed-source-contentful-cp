using Contentful.Core.Models;
using Contentful.Core.Models.Management;

namespace Enterspeed.Source.Contentful.CP.Services;

public interface IEntityIdentityService
{
    string GetId(Entry<dynamic> content);
    string GetId(Asset asset);
    string GetId(string id, Locale locale);
}