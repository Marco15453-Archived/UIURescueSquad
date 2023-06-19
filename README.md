# UIU Rescue Squad

A plugin that adds a new class to your server named "UIU Rescue Squad". Their job is helpping the NTF completing their task. They have a chance to spawn instead of a squad of NTF.

# Installation

**[EXILED](https://github.com/Exiled-Team/EXILED) must be installed for this to work.**

Place the `UIURescueSquad.dll` file in your EXILED/Plugins folder.

# Features

* Class has a configrable percent chance to spawn instead of NTF
* A configurable spawn location
* Commands to spawn individual members and a squad manually
* Announcements for a squad of UIU spawning, as well as two for ntf spawning to let the players know which one spawned

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
    glitch_chance: 0.05
    # Custom Cassie jam chance.
    jam_chance: 0.05
```