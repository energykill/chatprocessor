<div align="center">
  <img src="https://pan.samyyc.dev/s/VYmMXE" />
  <h2><strong>ChatProcessor</strong></h2>
  <h3>Transform your in-game chat with style and clarity.</h3>
</div>

<p align="center">
  <img src="https://img.shields.io/github/license/energykill/chatprocessor" alt="License">
  <img src="https://img.shields.io/github/actions/workflow/status/energykill/chatprocessor/deploy.yml" alt="Build Status">
  <img src="https://img.shields.io/github/v/release/energykill/chatprocessor?display_name=tag&label=release">
  <img src="https://img.shields.io/github/downloads/energykill/chatprocessor/total" alt="Downloads">
  <img src="https://img.shields.io/github/stars/energykill/chatprocessor?style=flat&logo=github" alt="Stars">
</p>

## About

**ChatProcessor** lets you fully customize the look and feel of your in-game chat. Highlight players, add colors, show locations, and create visually appealing messages for every event—whether it's a team chat, spectator message, or action alert. With easy configuration and powerful placeholders, you can make your chat truly unique.

***Key Features***:
- Fully customizable chat templates for all teams and spectators.
<!-- - Player-specific overrides to give VIPs or admins a distinct style. -->
- Support for dynamic placeholders like player name, location, time, and more.
- Rich color formatting with intuitive tags for messages and names.
- Handles in-game events like grenades, flashes, and other action notifications.

## Dependencies
To use this server addon, you'll need the following dependencies installed:

- [**SwiftlyS2**](https://github.com/swiftly-solution/swiftlys2/releases/latest): *SwiftlyS2 is a powerful scripting framework for Source 2 games, built in C++ with C# plugin support. It provides developers with a comprehensive API to create plugins for Source 2-based games like Counter-Strike 2*.

- [**PlaceholderAPI**](https://github.com/SwiftlyS2-Plugins/PlaceholderAPI): *A shared API to let you use placeholders in all the places you're thinking*.

## Building

- Open the project/solution in your preferred .NET IDE (e.g., Visual Studio, Rider, VS Code).
- Build the project. The output DLL and resources will be placed in the `build/` directory.

## Publishing

- Use the `dotnet publish -c Release` command to build and package your plugin.
- Distribute the contents of the `build/publish` directory.

## API Reference
TODO.

## Configuration

- After the plugin is successfully loaded, a configuration file will be created at `addons/swiftlys2/configs/plugins/ChatProcessor/config.jsonc`
```jsonc
{
  "ChatProcessor": {
    "Enabled": true,
    "MessageTokens": {
      "Cstrike_Chat_T": "[T] {NAME}: {MESSAGE}",
      "Cstrike_Chat_T_Loc": "[T] {NAME} [green]@{LOCATION}[/]: {MESSAGE}",
      "Cstrike_Chat_T_Dead": "[T] {NAME} [grey][DEAD][/]: {MESSAGE}",
      "Cstrike_Chat_CT": "[CT] {NAME}: {MESSAGE}",
      "Cstrike_Chat_CT_Loc": "[CT] {NAME} [green]@{LOCATION}[/]: {MESSAGE}",
      "Cstrike_Chat_CT_Dead": "[CT] {NAME} [grey][DEAD][/]: {MESSAGE}",
      "Cstrike_Chat_All": "[white][ALL] {NAME}: {MESSAGE}",
      "Cstrike_Chat_AllDead": "[white][ALL] {NAME}: {MESSAGE}",
      "Cstrike_Chat_Spec": "{NAME} [/][SPEC]: {MESSAGE}",
      "Cstrike_Chat_AllSpec": "[white][ALL] {NAME} [/][SPEC]: {MESSAGE}",
      "#SFUI_TitlesTXT_Fire_in_the_hole": "{NAME}[green]@{LOCATION}[red]\u279F HE Grenade!",
      "#SFUI_TitlesTXT_Molotov_in_the_hole": "{NAME}[green]@{LOCATION}[red]\u279F Molotov!",
      "#SFUI_TitlesTXT_Flashbang_in_the_hole": "{NAME}[green]@{LOCATION}[blue]\u279F Flashbang!",
      "#SFUI_TitlesTXT_Incendiary_in_the_hole": "{NAME}[green]@{LOCATION}[red]\u279F Incendiary!",
      "#SFUI_TitlesTXT_Smoke_in_the_hole": "{NAME}[green]@{LOCATION}[grey]\u279F Smoke!",
      "#SFUI_TitlesTXT_Decoy_in_the_hole": "{NAME}[green]@{LOCATION}[/]\u279F Decoy!"
    },
    "Players": {
      "76561198101370503":
      {
        "Priority": 10,
        "TagName": "[PLAYERTAG]",
        "TagColor": "blue",
        "NameColor": "red",
        "MessageColor": "red"
      }
    },
    "Permissions": {
      "chatprocessor.test":
      {
        "Priority": 100,
        "TagName": "[PERMISSIONTAG]",
        "TagColor": "blue",
        "NameColor": "red",
        "MessageColor": "red"
      }      
    }
  }
}
```
### Available tags for placeholders

#### Player Data supported only for ChatProcessor
| Tag | Description |
|-----|-------------|
| `{NAME}` | Player nickname (with appropriate color). |
| `{MESSAGE}` | Player chat message (with appropriate color). |
| `{LOCATION}` | Player location name. |

#### PlaceholderAPI

- ChatProcessor supports all placeholders registered via PlaceholderAPI, e.g., {MAPNAME}, etc

<!-- #### Time Formatting
Supports any valid `DateTime` format. [Read official documentation](https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings)

| Tag | Example Output | Description |
|-----|----------------|-------------|
| `{time:mm:ss}` | `12:07` | Minutes + seconds |
| `{time:HH:mm}` | `19:42` | 24-hour time |
| `{time:yyyy-MM-dd}` | `2025-11-17` | Date |
| `{time:HH:mm:ss.fff}` | `08:33:12.157` | Time with milliseconds | -->

#### Color Tags
Color tags change the color until another tag is used. Reset color with `[/]`.

| Tag | Color Name |
|-----|-------------|
| `[/]` | Reset color |
| `[white]` | White |
| `[darkred]` | Dark Red |
| `[green]` | Green |
| `[lightyellow]` | Light Yellow |
| `[lightblue]` | Light Blue |
| `[olive]` | Olive |
| `[lime]` | Lime |
| `[red]` | Red |
| `[lightpurple]` | Light Purple |
| `[purple]` | Purple |
| `[grey]` | Grey |
| `[yellow]` | Yellow |
| `[gold]` | Gold |
| `[silver]` | Silver |
| `[blue]` | Blue |
| `[darkblue]` | Dark Blue |
| `[bluegrey]` | Blue-grey |
| `[magenta]` | Magenta |
| `[lightred]` | Light Red |
| `[orange]` | Orange |

#### Team-Based Colors

| Tag | Description |
|-----|-------------|
| `[teamcolor]` | Team color (T = Yellow, CT = Blue, SPEC = White, None = White) |
| `[compcolor]` | Competitive teammate color |

### Player Overrides (Players)
The players section allows you to override name and message colors for specific players identified by their SteamID64.

| Field | Description |
|-------|-------------|
| Priority | A number indicating the override priority. Higher values mean the player's settings will take precedence over other rules. |
| TagName | Text displayed before the player's name (e.g., [ADMIN]). |
| TagColor | The color of the tag. You can use any predefined color, e.g., `blue`, `gold` |
| NameColor | The color used for the player's displayed name in chat. Accepts color tags like `red` or `purple`. |
| MessageColor | The color used for the player's chat messages. Also accepts color tags. |

Example:
```json
"Players": {
  "76561198101370503":
  {
    "Priority": 100,
    "TagName": "[PERMISSIONTAG]",
    "TagColor": "blue",
    "NameColor": "red",
    "MessageColor": "red"
  }  
}
```

### Permissions Overrides (Players)
The permissions section allows you to override name and message colors for specific permissions identified by their permission name.

| Field | Description |
|-------|-------------|
| Priority | A number indicating the override priority. Higher values mean the player's settings will take precedence over other rules. |
| TagName | Text displayed before the player's name (e.g., [ADMIN]). |
| TagColor | The color of the tag. You can use any predefined color, e.g., `blue`, `gold` |
| NameColor | The color used for the player's displayed name in chat. Accepts color tags like `red` or `purple`. |
| MessageColor | The color used for the player's chat messages. Also accepts color tags. |

Example:
```json
"Permissions": {
  "admin.chat":
  {
    "Priority": 100,
    "TagName": "[ADMIN]",
    "TagColor": "blue",
    "NameColor": "red",
    "MessageColor": "red"
  }  
}
```

This makes it possible to give selected players a unique visual chat style, regardless of global settings.