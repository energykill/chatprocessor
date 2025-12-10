using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using ChatProcessor.Infrastructure.Integrations;
using ChatProcessor.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Players;

namespace ChatProcessor.Managers;

internal class ChatManager
{
    private readonly ISwiftlyCore _core;
    private readonly ILogger<ChatManager> _logger;

    private ConcurrentDictionary<string, string> _messageTokens = new();
    private ConcurrentDictionary<ulong, ChatSettings> _playerSettings = new();
    private ConcurrentDictionary<string, ChatSettings> _permissionSettings = new();

    private ConcurrentDictionary<ulong, ChatSettings> _playerCachedSettings = new();

    public bool IsEnable { get; set; } = true;
    
    public ChatManager(ISwiftlyCore core, IOptionsMonitor<ChatConfig> options, ILogger<ChatManager> logger)
    {
        _core = core;
        _logger = logger;

        LoadChatOptions(options.CurrentValue);

        options.OnChange((config) =>
        {
            try
            {
                _logger.LogInformation("Chat config changed, reloading...");
                LoadChatOptions(config);
                _logger.LogInformation("Chat config reloaded.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reloading chat config.");
            }
        });
    }

    public string GetTokenPlaceholder(string token) => _messageTokens.TryGetValue(token, out var placeholder) ? placeholder : token;

    private void LoadChatOptions(ChatConfig config)
    {
        _messageTokens.Clear();
        _playerSettings.Clear();
        _permissionSettings.Clear();
        _playerCachedSettings.Clear();

        _messageTokens = new(config.MessageTokens);
        _playerSettings = new(config.Players.ToDictionary(x => ulong.Parse(x.Key), x => x.Value));
        _permissionSettings = new(config.Permissions);
        IsEnable = config.Enabled;
    }

    public ChatSettings GetPlayerSettings(IPlayer player)
    {
        if(_playerCachedSettings.TryGetValue(player.SteamID, out var cached))
            return cached;
        
        var settings = new ChatSettings
        {
            NameColor = "teamcolor",
            MessageColor = "white"
        };

        foreach (var permissionItem in _permissionSettings)
        {
            if(_core.Permission.PlayerHasPermission(player.SteamID, permissionItem.Key)) {
                MergeSettings(settings, permissionItem.Value);
            }         
        }

        if(_playerSettings.TryGetValue(player.SteamID, out var playerSettings))
        {
            MergeSettings(settings, playerSettings);
        }

        _playerCachedSettings[player.SteamID] = settings;

        return settings;
    }

    private static void MergeSettings(ChatSettings target, ChatSettings source)
    {
        if (source == null) return;

        if (source.Priority < target.Priority) return;

        if (!string.IsNullOrEmpty(source.NameColor)) target.NameColor = source.NameColor;

        if (!string.IsNullOrEmpty(source.MessageColor)) target.MessageColor = source.MessageColor;

        if (!string.IsNullOrEmpty(source.TagName)) target.TagName = source.TagName;

        if (!string.IsNullOrEmpty(source.TagColor)) target.TagColor = source.TagColor;

        if (source.Priority > target.Priority) target.Priority = source.Priority;
    }

}