using ChatProcessor.Infrastructure.Integrations;
using ChatProcessor.Managers;
using ChatProcessor.Models;
using ChatProcessor.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlaceholderAPI.Contract;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Plugins;

namespace ChatProcessor;

[PluginMetadata(
    Id = "ChatProcessor",
    Name = "ChatProcessor",
    Version = "0.0.0",
    Author = "EnergyKill & Contributors",
    Website = "github.com/energykill/chatprocessor"
)]
public class Bootstrap(ISwiftlyCore core) : BasePlugin(core)
{
    private ServiceCollection? _service;
    private ServiceProvider? _provider;

    public override void Load(bool hotReload)
    {
        Core.Configuration
            .InitializeJsonWithModel<ChatConfig>("config.jsonc", "ChatProcessor")
            .Configure( builder =>
            {
                builder.AddJsonFile("config.jsonc", optional: false, reloadOnChange: true);
            });
        
        _service = new();

        _service.
            AddOptions<ChatConfig>()
            .BindConfiguration("ChatProcessor")
            .ValidateOnStart();

        _service
            .AddSwiftly(Core)
            .AddSingleton<PlaceholderApiAccessor>()
            .AddSingleton<ChatManager>()
            .AddSingleton<ChatHookService>();

        _provider = _service.BuildServiceProvider();
        
        _ = _provider.GetRequiredService<PlaceholderApiAccessor>();
        _ = _provider.GetRequiredService<ChatManager>();
        _ = _provider.GetRequiredService<ChatHookService>();
    }

    public override void ConfigureSharedInterface(IInterfaceManager interfaceManager)
    {
        // TODO API REFERENCE
    }

    public override void OnSharedInterfaceInjected(IInterfaceManager interfaceManager)
    {
        var placeholderAPI = interfaceManager.GetSharedInterface<IPlaceholderAPIv1>("PlaceholderAPI.v1");
        if (placeholderAPI == null)
        {
            Core.Logger.LogError("PlaceholderAPI is not available.");
            return;
        }
        var accessor = _provider!.GetRequiredService<PlaceholderApiAccessor>();
        accessor.Set(placeholderAPI);
    }

    public override void Unload()
    {
        _provider!.Dispose();
        _service = null;
    }
}
