using Enterspeed.Source.Contentful.CP.Models.Configuration;

namespace Enterspeed.Source.Contentful.CP.Services;

public interface IEnterspeedConfigurationService
{
    EnterspeedContentfulConfiguration GetConfiguration();
}