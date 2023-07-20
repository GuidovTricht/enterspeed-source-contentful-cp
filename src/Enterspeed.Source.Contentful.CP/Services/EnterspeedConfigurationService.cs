using Enterspeed.Source.Contentful.CP.Models.Configuration;
using System;

namespace Enterspeed.Source.Contentful.CP.Services;

public class EnterspeedConfigurationService : IEnterspeedConfigurationService
{
    public EnterspeedContentfulConfiguration GetConfiguration()
    {
        return new EnterspeedContentfulConfiguration
        {
            ApiKey = Environment.GetEnvironmentVariable("Enterspeed.ApiKey"),
            BaseUrl = Environment.GetEnvironmentVariable("Enterspeed.BaseUrl"),
            ContentfulDeliveryApiKey = Environment.GetEnvironmentVariable("Contentful.DeliveryApiKey"),
            ContentfulPreviewApiKey = Environment.GetEnvironmentVariable("Contentful.PreviewApiKey"),
            ContentfulSpaceId = Environment.GetEnvironmentVariable("Contentful.SpaceId")
        };
    }
}