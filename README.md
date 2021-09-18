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
* Compatible with [RespawnTimer](https://github.com/Michal78900/RespawnTimer) and [CustomItems](https://github.com/Exiled-Team/CustomItems)

# Configs
```yml
# Is the plugin enabled.
  is_enabled: true
  # Should debug messages be shown in a server console.
  debug: false
  ```

## SpawnManager
Configs for UIU spawning options.
```yml
spawn_manager:
  # How many mtfs respawns must have happened to spawn UIU
    respawns: 1
    # Probability of a UIU Squad replacing a MTF spawn
    probability: 50
    # The maximum size of a UIU squad
    max_squad: 8
    # UIU Rescue squad spawn position:
    spawn_pos:
      x: 170
      y: 985
      z: 29
    # Entrance broadcast announcement message (null to disable it)
    announcement_text: ''
    # Entrance broadcast announcement message time
    announcement_time: 10
    # UIU entrance Cassie Message
    uiu_announcement_cassie: The U I U Squad designated {designation} HasEntered AwaitingRecontainment {scpnum}
    uiu_announcment_cassie_no_scp: The U I U Squad designated {designation} HasEntered NoSCPsLeft
    # NTF entrance Cassie Message (leave empty to use default NTF cassie entrance)
    ntf_announcement_cassie: ''
    ntf_announcment_cassie_no_scp: ''
    # Custom Cassie glitch chance.
    glitch_chance: 0.0500000007
    # Custom Cassie jam chance.
    jam_chance: 0.0500000007
    # Use hints instead of broadcasts.
    use_hints: false
    # UIU Player broadcast (null to disable it)
    uiu_broadcast: <i>You are an</i><color=yellow><b> UIU trooper</b></color>, <i>help </i><color=#0377fc><b>MTFs</b></color><i> to finish its job</i>
    # UIU Player broadcast time
    uiu_broadcast_time: 10
```

## ClassConfig
There are 3 class configs foreach UIU role and they are identical.
```yml
    health:
    inventory:
    - KeycardNTFLieutenant
    - GunCrossvec
    - GunCOM18
    - Medkit
    - Adrenaline
    - Radio
    - GrenadeFrag
    - ArmorCombat
    ammo:
      Nato556: 80
      Nato762: 0
      Nato9: 100
    rank: UIU Leader
```

## TeamColors
Config for overrding existing unit names.
```yml
  team_colors:
  # Custom guard color (leave empty for default color)
    guard_unit_color: '#797D7F'
    # Custom NTF color (leave empty for default color)
    ntf_unit_color: '#0887E5'
    # Custom UIU color (leave empty for default color)
    uiu_unit_color: yellow
```

## SupplyDrop
```yml
  # Option for UIU supply drop:
  supply_drop:
  # Should a drop spawn with UIUs
    drop_enabled: false
    # List of items that appears in a drop (supports CustomItems)
    drop_items:
      Medkit: 1
      Ammo556: 2
```
# Commands
All UIU Rescue Squad commands begins with `uiu` prefix.
| Command | Prefix | Required permission | Description | Example |
| :-------------: | :---------: | :---------: | :---------: | :---------:
| **list** | l | `uiu.list` | Shows the list of players that are currently UIU with their UIU role and unit name. | `uiu l`
| **spawn** | s | `uiu.spawn` | Makes the player an UIU. (it uses IDs / nicknames as argument, if no argument is given, it will make the Command Sender a UIU Leader, the second argument is UIU role) | `uiu s  `
| **spawnteam** | st | `uiu.spawnteam` | Spawns UIU team with given number of players (if no argument is given it will try to spawn a squad with max number provided in a config) **Keep in mind this command won't work if there is not enough Spectators** | `uiu st`