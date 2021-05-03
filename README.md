# UIU Rescue Squad

A plugin that adds a new class to your server named "UIU Rescue Squad". Their job is helpping the NTF completing their task. They have a chance to spawn instead of a squad of NTF.

# Installation

**[EXILED](https://github.com/galaxy119/EXILED) must be installed for this to work.**

Place the `UIURescueSquad.dll` file in your EXILED/Plugins folder.

# Features

* Class has a configrable percent chance to spawn instead of NTF
* A configurable spawn location
* Commands to spawn individual members and a squad manually
* Announcements for a squad of UIU spawning, as well as two for ntf spawning to let the players know which one spawned
* Custom API for other plugins to interact with
* Compatible with [RespawnTimer](https://github.com/Michal78900/RespawnTimer)

# Configs
| Config        | Value Type | Default Value | Description |
| :-------------: | :---------: | :------: | :--------- |
| `is_enabled` | bool | true | Is the plugin enabled.
| `debug` | bool | false | Should debug messages be shown in a server console.

## SpawnManager
Configs for UIU spawning options.
| Config        | Value Type | Default Value | Description |
| :-------------: | :---------: | :------: | :--------- |
| `respawns` | int | 1 | How many mtfs respawns must have happened to spawn UIU |
| `probability` | int | 50 | Probability of a UIU Squad replacing a MTF spawn |
| `max_squad` | uint | 8 | The maximum size of a UIU squad |
| `spawn_pos` | Vector | x: 170, y: 985, z: 29 | UIU Rescue squad spawn position |
| `announcement_text` | string | '' | Entrance broadcast announcement message |
| `announcement_time` | ushort | 10 | Entrance broadcast announcement message duration time |
| `uiu_announcement_cassie` | string | The U I U Squad HasEntered AwaitingRecontainment {scpnum} | UIU entrance Cassie Message |
| `uiu_announcment_cassie_no_scp` | string | The U I U Squad HasEntered NoSCPsLeft | UIU entrance Cassie Message |
| `ntf_announcement_cassie` | string | '' | NTF entrance Cassie Message (leave empty to use default NTF cassie entrance) |
| `ntf_announcement_cassie` | string | '' | NTF entrance Cassie Message (leave empty to use default NTF cassie entrance) |
| `glitch_chance` | float | 0.05 | Custom Cassie glitch chance |
| `jam_chance` | float | 0.05 | Custom Cassie glitch chance |
| `use_hints` | bool | false | Use hints instead of broadcasts |
| `uiu_broadcast` | string | \<i>You are an\</i>\<color=yellow>\<b> UIU trooper\</b>\</color>, \<i>help \</i>\<color=#0377fc>\<b>MTFs\</b>\</color>\<i> to finish its job\</i> | UIU Player broadcast (null to disable it) |
| `uiu_broadcast_time` | ushort | 10 | UIU Player broadcast time |

## ClassConfig
There are 3 class configs foreach UIU role and they are identical.
| Config        | Value Type | Default Value | Description |
| :-------------: | :---------: | :------: | :--------- |
| `health` | float | - | UIU Health |
| `inventory` | List\<string> | - | UIU Invenotory (supports [CustomItems](https://github.com/Exiled-Team/CustomItems)) |
| `ammo` | Dictionary\<AmmoType, uint> | - | UIU Ammo |
| `rank` | string | - | UIU Rank seen in-game by other players |

## TeamColors
Config for overrding existing unit names.
| Config        | Value Type | Default Value | Description |
| :-------------: | :---------: | :------: | :--------- |
| `guard_unit_color` | string | #797D7F (grey) | Custom guard color |
| `ntf_unit_color` | string | #0887E5 (blue) | Custom NTF color |
| `uiu_unit_color` | string | yellow | Custom UIU color |

## SupplyDrop
Config for supply drop, when the UIU has respawned. This is disabled by default.
| Config        | Value Type | Default Value | Description |
| :-------------: | :---------: | :------: | :--------- |
| `drop_enabled` | bool | false | Toggles UIU's supply drop |
| `drop_items` | Dictionary<string, uint> | Medkit: 1, Ammo556: 2, Disarmer: 1 | Items that will be in UIU's supply drop. (supports [CustomItems](https://github.com/Exiled-Team/CustomItems)) |

# Commands
All UIU Rescue Squad commands begins with `uiu` prefix.
| Command | Prefix | Required permission | Description | Example |
| :-------------: | :---------: | :---------: | :---------: | :---------:
| **list** | l | `uiu.list` | Shows the list of players that are currently UIU with their UIU role and unit name. | `uiu l`
| **spawn** | s | `uiu.spawn` | Makes the player an UIU. (it uses IDs / nicknames as argument, if no argument is given, it will make the Command Sender a UIU Leader, the second argument is UIU role) | `uiu s  `
| **spawnteam** | st | `uiu.spawnteam` | Spawns UIU team with given number of players (if no argument is given it will try to spawn a squad with max number provided in a config) **Keep in mind this command won't work if there is not enough Spectators** | `uiu st`