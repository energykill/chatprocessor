using System.Text.Json.Serialization;

namespace ChatProcessor.Models;

public class ChatConfig
{
    public bool Enabled { get; set; } = true;
    public Dictionary<string,string> MessageTokens { get; set; } = new()
    {
        { "Cstrike_Chat_T", "[T] {NAME}: {MESSAGE}" },
        { "Cstrike_Chat_T_Loc", "[T] {NAME} [green]@{LOCATION}[/]: {MESSAGE}" },
        { "Cstrike_Chat_T_Dead", "[T] {NAME} [grey][DEAD][/]: {MESSAGE}" },

        { "Cstrike_Chat_CT", "[CT] {NAME}: {MESSAGE}" },
        { "Cstrike_Chat_CT_Loc", "[CT] {NAME} [green]@{LOCATION}[/]: {MESSAGE}" },
        { "Cstrike_Chat_CT_Dead", "[CT] {NAME} [grey][DEAD][/]: {MESSAGE}" },

        { "Cstrike_Chat_All", "[white][ALL] {NAME}: {MESSAGE}" },
        { "Cstrike_Chat_AllDead", "[white][ALL] {NAME}: {MESSAGE}" },
        { "Cstrike_Chat_Spec", "{NAME} [/][SPEC]: {MESSAGE}" },
        { "Cstrike_Chat_AllSpec", "[white][ALL] {NAME} [/][SPEC]: {MESSAGE}" },

        { "#SFUI_TitlesTXT_Fire_in_the_hole", "{NAME}[green]@{LOCATION}[red]➟ HE Grenade!" },
        { "#SFUI_TitlesTXT_Molotov_in_the_hole", "{NAME}[green]@{LOCATION}[red]➟ Molotov!" },
        { "#SFUI_TitlesTXT_Flashbang_in_the_hole", "{NAME}[green]@{LOCATION}[blue]➟ Flashbang!" },
        { "#SFUI_TitlesTXT_Incendiary_in_the_hole", "{NAME}[green]@{LOCATION}[red]➟ Incendiary!" },
        { "#SFUI_TitlesTXT_Smoke_in_the_hole", "{NAME}[green]@{LOCATION}[grey]➟ Smoke!" },
        { "#SFUI_TitlesTXT_Decoy_in_the_hole", "{NAME}[green]@{LOCATION}[/]➟ Decoy!" }

    };
    public Dictionary<string, ChatSettings> Players { get; set; } = [];
    public Dictionary<string,ChatSettings> Permissions { get; set; } = [];
    
}
public class ChatSettings
{
    public int Priority { get; set; } = 0;
    public string? TagName { get; set; }
    public string? TagColor { get; set; }
    public string? NameColor { get; set; }
    public string? MessageColor { get; set; }
    
}