using System.Text;
using ChatProcessor.Extensions;
using ChatProcessor.Infrastructure.Integrations;
using ChatProcessor.Managers;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace ChatProcessor.Services;


internal class ChatHookService
{
    private readonly ISwiftlyCore _core;
    private readonly ChatManager _chatManager;


    private readonly PlaceholderApiAccessor _placeholder;

    public ChatHookService(ISwiftlyCore core, ChatManager chatManager, PlaceholderApiAccessor placeholderApiAccessor)
    {
        _core = core;
        _chatManager = chatManager;
        _placeholder = placeholderApiAccessor;
        core.Registrator.Register(this);
    }

    [ServerNetMessageHandler]
    public HookResult OnSayText2MessageHandler(CUserMessageSayText2 msg)
    {
        if(!_chatManager.IsEnable) return HookResult.Continue;

        var player = _core.PlayerManager.GetPlayer(msg.Entityindex - 1);
        if (player == null || player.IsFakeClient) return HookResult.Continue;

        var newMessage = FormatMessage(player, msg.Messagename);

        if (newMessage.Equals(msg.Messagename)) return HookResult.Continue;
        msg.Messagename = newMessage;
        return HookResult.Continue;
    }

    [ServerNetMessageHandler]
    public HookResult OnRadioTextMessageHandler(CCSUsrMsg_RadioText msg)
    {
        if(!_chatManager.IsEnable) return HookResult.Continue;

        var player = _core.PlayerManager.GetPlayer(msg.Client);
        if (player == null || player.IsFakeClient) return HookResult.Continue;

        var token =  msg.Params[2];
        var newMessage = FormatMessage(player, token, true);

        if (newMessage.Equals(token)) return HookResult.Continue;
        msg.MsgName = newMessage;
        
        return HookResult.Continue;
    }

    private string FormatMessage(IPlayer player, string token, bool isRadio = false)
    {
        string msgPlaceholder = _chatManager.GetTokenPlaceholder(token);
        if (msgPlaceholder.Equals(token)) return token;

        var playerSettings = _chatManager.GetPlayerSettings(player);

        StringBuilder tag = new();

        if (!string.IsNullOrEmpty(playerSettings.TagName))
        {
            if(!string.IsNullOrEmpty(playerSettings.TagColor))
            {
                tag.Append($"[{playerSettings.TagColor}]");
            }
            tag.Append(playerSettings.TagName);
        }
        StringBuilder name = new();
        name.Append(tag.ToString());

        if(!string.IsNullOrEmpty(playerSettings.NameColor))
        {
            name.Append($"[{playerSettings.NameColor}]");
        }

        name.Append(isRadio ? player.Controller.PlayerName : "%s1");

        StringBuilder message = new();

        if(!isRadio)
        {
            if(!string.IsNullOrEmpty(playerSettings.MessageColor))
            {
                message.Append($"[{playerSettings.MessageColor}]");
            }

            message.Append("%s2");
        }

        if(_placeholder.Api != null)
        {
            msgPlaceholder = _placeholder.Api.ProcessMessage(player, msgPlaceholder);
        }
        
        msgPlaceholder = msgPlaceholder
            .Replace("{NAME}", name.ToString())
            .Replace("{LOCATION}", isRadio ? "%s2" : "%s3")
            .Replace("{MESSAGE}", message.ToString())
            .Replace("[teamcolor]", $"[{player.GetTeamChatColor()}]")
            .Replace("[compcolor]", $"[{player.GetCompTeammateChatColor()}]");

        return Helper.Colored(msgPlaceholder);
    }
}