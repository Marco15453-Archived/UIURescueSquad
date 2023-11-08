# This Plugin has been **ABANDONED**
# UIU Rescue Squad

A plugin that adds a new class to your server named "UIU Rescue Squad". Their job is helpping the NTF completing their task. They have a chance to spawn instead of a squad of NTF.

# Installation

**[EXILED](https://github.com/Exiled-Team/EXILED) must be installed for this to work.**

**CustomRoles need to be enabled on your server for this to work.**

Place the `UIURescueSquad.dll` file in your EXILED/Plugins folder.

# Features

* Class has a configrable percent chance to spawn instead of NTF
* A configurable spawn location
* Commands to spawn individual members and a squad manually
* Announcements for a squad of UIU spawning, as well as two for ntf spawning to let the players know which one spawned
* Compatible with [RespawnTimer](https://github.com/Michal78900/RespawnTimer)

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
  # How many respawn waves must occur before considering UIU to spawn.
    respawns: 1
    # The maximum number of times UIU Rescue Squad can spawn per game.
    max_spawns: 3
    # Probability of a UIU Squad replacing a MTF spawn
    probability: 50
    # The maximum size of a UIU squad
    max_squad: 8
    # UIU entrance Cassie Message
    uiu_announcement_cassie: 'The U I U Squad designated {designation} HasEntered AllRemaining AwaitingRecontainment {scpnum}'
    uiu_announcment_cassie_no_scp: 'The U I U Squad designated {designation} HasEntered AllRemaining NoSCPsLeft'
    # NTF entrance Cassie Message (leave empty to use default NTF cassie entrance)
    ntf_announcement_cassie: 'MTFUnit epsilon 11 designated {designation} hasentered AllRemaining AwaitingRecontainment {scpnum}'
    ntf_announcment_cassie_no_scp: 'MTFUnit epsilon 11 designated {designation} hasentered AllRemaining NoSCPsLeft'
    # Custom Cassie glitch chance.
    glitch_chance: 0.0500000007
    # Custom Cassie jam chance.
    jam_chance: 0.0500000007
```

## Classes
```yml
  uiu_leader:
    id: 3
    role: NtfCaptain
    max_health: 150
    name: 'UIU Leader'
    description: 'Help MTFs to finish their job'
    custom_info: 'UIU Leader'
    inventory:
    - 'KeycardNTFLieutenant'
    - 'GunCrossvec'
    - 'GunCOM18'
    - 'Medkit'
    - 'Adrenaline'
    - 'Radio'
    - 'GrenadeHE'
    - 'ArmorCombat'
    ammo:
      Nato556: 80
      Nato9: 100
    spawn_properties:
      limit: 0
      dynamic_spawn_points: []
      static_spawn_points: []
      role_spawn_points:
      - role: NtfCaptain
        chance: 100
    custom_abilities: []
    keep_position_on_spawn: false
    keep_inventory_on_spawn: false
    removal_kills_player: true
    keep_role_on_death: false
    spawn_chance: 0
    ignore_spawn_system: false
    keep_role_on_changing_role: false
    broadcast:
    # The broadcast content
      content: ''
      # The broadcast duration
      duration: 10
      # The broadcast type
      type: Normal
      # Indicates whether the broadcast should be shown or not
      show: true
    display_custom_item_messages: true
    scale:
      x: 1
      y: 1
      z: 1
    custom_role_f_f_multiplier: {}
    console_message: 'You have spawned as a custom role!'
    ability_usage: 'Enter ".special" in the console to use your ability. If you have multiple abilities, you can use this command to cycle through them, or specify the one to use with ".special ROLENAME AbilityNum"'
  # Options for UIU Agent:
  uiu_agent:
    id: 2
    role: NtfSergeant
    max_health: 130
    name: 'UIU Agent'
    description: 'Help MTFs to finish their job'
    custom_info: 'UIU Agent'
    inventory:
    - 'KeycardNTFLieutenant'
    - 'GunCrossvec'
    - 'GunCOM18'
    - 'Medkit'
    - 'Adrenaline'
    - 'Radio'
    - 'GrenadeHE'
    - 'ArmorCombat'
    ammo:
      Nato556: 80
      Nato9: 100
    spawn_properties:
      limit: 0
      dynamic_spawn_points: []
      static_spawn_points: []
      role_spawn_points:
      - role: NtfSergeant
        chance: 100
    custom_abilities: []
    keep_position_on_spawn: false
    keep_inventory_on_spawn: false
    removal_kills_player: true
    keep_role_on_death: false
    spawn_chance: 0
    ignore_spawn_system: false
    keep_role_on_changing_role: false
    broadcast:
    # The broadcast content
      content: ''
      # The broadcast duration
      duration: 10
      # The broadcast type
      type: Normal
      # Indicates whether the broadcast should be shown or not
      show: true
    display_custom_item_messages: true
    scale:
      x: 1
      y: 1
      z: 1
    custom_role_f_f_multiplier: {}
    console_message: 'You have spawned as a custom role!'
    ability_usage: 'Enter ".special" in the console to use your ability. If you have multiple abilities, you can use this command to cycle through them, or specify the one to use with ".special ROLENAME AbilityNum"'
  # Options for UIU Soldier:
  uiu_soldier:
    id: 1
    role: NtfPrivate
    max_health: 120
    name: 'UIU Soldier'
    description: 'Help MTFs to finish their job'
    custom_info: 'UIU Soldier'
    inventory:
    - 'KeycardNTFLieutenant'
    - 'GunCrossvec'
    - 'GunCOM18'
    - 'Medkit'
    - 'Adrenaline'
    - 'Radio'
    - 'GrenadeHE'
    - 'ArmorCombat'
    ammo:
      Nato556: 80
      Nato9: 100
    spawn_properties:
      limit: 0
      dynamic_spawn_points: []
      static_spawn_points: []
      role_spawn_points:
      - role: NtfPrivate
        chance: 100
    custom_abilities: []
    keep_position_on_spawn: false
    keep_inventory_on_spawn: false
    removal_kills_player: true
    keep_role_on_death: false
    spawn_chance: 0
    ignore_spawn_system: false
    keep_role_on_changing_role: false
    broadcast:
    # The broadcast content
      content: ''
      # The broadcast duration
      duration: 10
      # The broadcast type
      type: Normal
      # Indicates whether the broadcast should be shown or not
      show: true
    display_custom_item_messages: true
    scale:
      x: 1
      y: 1
      z: 1
    custom_role_f_f_multiplier: {}
    console_message: 'You have spawned as a custom role!'
    ability_usage: 'Enter ".special" in the console to use your ability. If you have multiple abilities, you can use this command to cycle through them, or specify the one to use with ".special ROLENAME AbilityNum"'
```
