using Contentful.Core.Models;
using Newtonsoft.Json;

namespace Enterspeed.Source.Contentful.CP.Models;

public class ContentfulResource : IContentfulResource
{
    [JsonProperty("sys")]
    public SystemProperties SystemProperties { get; set; }
}