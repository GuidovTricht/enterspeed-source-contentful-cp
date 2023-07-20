using Enterspeed.Source.Sdk.Configuration;

namespace Enterspeed.Source.Contentful.CP.Models.Configuration;

public class EnterspeedContentfulConfiguration : EnterspeedConfiguration
{
    public string ContentfulDeliveryApiKey { get; set; }
    public string ContentfulPreviewApiKey { get; set; }
    public string ContentfulSpaceId { get; set; }
}