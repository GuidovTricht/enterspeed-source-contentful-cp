using Enterspeed.Source.Contentful.CP;
using Enterspeed.Source.Contentful.CP.Factories;
using Enterspeed.Source.Contentful.CP.Handlers;
using Enterspeed.Source.Contentful.CP.Providers;
using Enterspeed.Source.Contentful.CP.Services;
using Enterspeed.Source.Contentful.CP.Services.FieldValueConverters;
using Enterspeed.Source.Sdk.Api.Connection;
using Enterspeed.Source.Sdk.Api.Providers;
using Enterspeed.Source.Sdk.Api.Services;
using Enterspeed.Source.Sdk.Domain.Connection;
using Enterspeed.Source.Sdk.Domain.Services;
using Enterspeed.Source.Sdk.Domain.SystemTextJson;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Enterspeed.Source.Contentful.CP;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        builder.Services.AddSingleton<IEnterspeedConfigurationService, EnterspeedConfigurationService>();
        builder.Services.AddSingleton<IEnterspeedConfigurationProvider, EnterspeedContentfulConfigurationProvider>();
        builder.Services.AddSingleton<IEnterspeedConnection, EnterspeedConnection>();
        builder.Services.AddSingleton<IEnterspeedIngestService, EnterspeedIngestService>();
        builder.Services.AddSingleton<IEnterspeedPropertyService, EnterspeedPropertyService>();
        builder.Services.AddSingleton<IJsonSerializer, SystemTextJsonSerializer>();
        builder.Services.AddSingleton<IContentfulClientService, ContentfulClientService>();
        builder.Services.AddSingleton<IEntityIdentityService, EntityIdentityService>();
        builder.Services.AddSingleton<IContentfulFieldFactory, ContentfulFieldFactory>();

        // Field value converters
        builder.Services.AddSingleton<IEnterspeedFieldValueConverter, StringFieldValueConverter>();
        builder.Services.AddSingleton<IEnterspeedFieldValueConverter, NumberFieldValueConverter>();
        builder.Services.AddSingleton<IEnterspeedFieldValueConverter, BooleanFieldValueConverter>();
        builder.Services.AddSingleton<IEnterspeedFieldValueConverter, ObjectFieldValueConverter>();
        builder.Services.AddSingleton<IEnterspeedFieldValueConverter, ArrayFieldValueConverter>();

        // Event handlers
        builder.Services.AddSingleton<IEnterspeedEventHandler, EntryPublishEventHandler>();
        builder.Services.AddSingleton<IEnterspeedEventHandler, EntryDeleteEventHandler>();
        builder.Services.AddSingleton<IEnterspeedEventHandler, AssetPublishEventHandler>();
        builder.Services.AddSingleton<IEnterspeedEventHandler, AssetDeleteEventHandler>();
        builder.Services.AddSingleton<IEnterspeedEventHandler, SeedEventHandler>();
    }
}