using ChatProcessor.Enums;
using SwiftlyS2.Shared.Players;

namespace ChatProcessor.Extensions;

public static class PlayerExtension
{
    public static string GetName(this IPlayer player)
    {
        return player.Controller?.PlayerName ?? "Unknown";
    }

    public static string GetCompTeammateChatColor(this IPlayer player)
    {
        string color = (CompTeammateColor_t)player.Controller.CompTeammateColor switch
        {
            CompTeammateColor_t.GREY => "grey",
            CompTeammateColor_t.BLUE => "blue",
            CompTeammateColor_t.GREEN => "green",
            CompTeammateColor_t.YELLOW => "yellow",
            CompTeammateColor_t.ORANGE => "orange",
            CompTeammateColor_t.PURPLE => "purple",
            _ => "white",
        };
        return color;
    }

    public static string GetTeamChatColor(this IPlayer player)
    {
        if (player == null || player is { IsValid: false }) return "white";

        string color = player.Controller.TeamNum switch
        {
            (byte)Team.T => "yellow",
            (byte)Team.CT => "blue",
            (byte)Team.Spectator => "grey",
            _ => "White",            
        };

        return color;
    }
    
}