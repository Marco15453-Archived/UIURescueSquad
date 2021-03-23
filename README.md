# UIURescueSquad
 A new exiled plugin that add a new GOI to the game [FBI/UIU]

# Installation
Download the .dll file of the latest release and place it inside the Exiled Plugins folder

# Configs
```yaml
UIURescueSquad:
# Is the plugin enabled?
  is_enabled: true
  # Should debug messages be shown in a server console
  debug: false
  # How many mtfs respawns must have happened to spawn UIU
  respawns: 1
  # Probability of a UIU Squad replacing a MTF spawn
  probability: 50
  # The maximum size of a UIU squad
  max_squad: 8
  # Should a drop spawn with UIUs
  drop_enabled: true
  # List of items that appears in a drop
  drop_items:
  - Medkit
  - Painkillers
  - Radio
  - Ammo556
  - Disarmer
  # UIU Rescue squad spawn position:
  spawn_pos_x: 170
  spawn_pos_y: 985
  spawn_pos_z: 29
  # Entrance broadcast announcement message (null to disable it)
  announcement_text: <b>The <color=#FFFA4B>UIU Rescue Squad</color> has arrived to the facility</b>
  # Entrance broadcast announcement message time
  announcement_time: 10
  # UIU entrance Cassie Message
  uiu_announcement_cassie: The U I U Squad HasEntered AwaitingRecontainment {scpnum}
  uiu_announcment_cassie_no_scp: The U I U Squad HasEntered NoSCPsLeft
  # NTF entrance Cassie Message (leave empty to use default NTF cassie entrance)
  ntf_announcement_cassie: ''
  ntf_announcment_cassie_no_scp: ''
  # Use hints instead of broadcasts?
  use_hints_here: false
  # UIU Player broadcast (null to disable it)
  uiu_broadcast: <i>You are an</i><color=yellow><b> UIU trooper</b></color>, <i>help </i><color=#0377fc><b>MTFs</b></color><i> to finish its job</i>
  # UIU Player broadcast (null to disable it)
  uiu_broadcast_time: 10
  # UIU Soldier life (NTF CADET)
  uiu_soldier_life: 160
  # The items UIUs soldiers spawn with
  uiu_soldier_inventory:
  - KeycardNTFLieutenant
  - GunProject90
  - GunUSP
  - Disarmer
  - Medkit
  - Adrenaline
  - Radio
  - GrenadeFrag
  # Ammo UIUs soldiers spawn with.
  uiu_soldier_ammo:
    Nato556: 80
    Nato762: 0
    Nato9: 100
  # UIU Soldier Rank (instead of Nine-Tailed Fox Cadet role)
  uiu_soldier_rank: UIU Soldier
  # UIU Agent life (NTF LIEUTENANT)
  uiu_agent_life: 175
  # The items UIUs agents spawn with
  uiu_agent_inventory:
  - KeycardNTFLieutenant
  - GunProject90
  - GunUSP
  - Disarmer
  - Medkit
  - Adrenaline
  - Radio
  - GrenadeFrag
  # Ammo UIUs agents spawn with.
  u_i_u_agent_ammo:
    Nato556: 80
    Nato762: 0
    Nato9: 100
  # UIU Agent Rank (instead of Nine-Tailed Fox Lieutenant role)
  uiu_agent_rank: UIU Agent
  # UIU Leader life (NTF COMMANDER)
  uiu_leader_life: 215
  # The items UIU leader spawn with.
  uiu_leader_inventory:
  - KeycardNTFLieutenant
  - GunProject90
  - GunUSP
  - Disarmer
  - Medkit
  - Adrenaline
  - Radio
  - GrenadeFrag
  # Ammo UIUs leaders spawn with.
  uiu_leader_ammo:
    Nato556: 80
    Nato762: 0
    Nato9: 100
  # UIU Leader Rank (instead of Nine-Tailed Fox Commander role)
  uiu_leader_rank: UIU Leader
  # Should plugin change colors for units? (leave empty for default color)
  guard_unit_color: '#797D7F'
  ntf_unit_color: '#0887E5'
  uiu_unit_color: yellow
```
