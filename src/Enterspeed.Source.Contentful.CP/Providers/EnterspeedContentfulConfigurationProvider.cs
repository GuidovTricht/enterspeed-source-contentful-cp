using Enterspeed.Source.Contentful.CP.Services;
using Enterspeed.Source.Sdk.Api.Providers;
using Enterspeed.Source.Sdk.Configuration;

namespace Enterspeed.Source.Contentful.CP.Providers;

public class EnterspeedContentfulConfigurationProvider : IEnterspeedConfigurationProvider
{
    private readonly IEnterspeedConfigurationService _enterspeedConfigurationService;

    public EnterspeedContentfulConfigurationProvider(IEnterspeedConfigurationService enterspeedConfigurationService)
    {
        _enterspeedConfigurationService = enterspeedConfigurationService;
    }
    public EnterspeedConfiguration Configuration => _enterspeedConfigurationService.GetConfiguration();
}