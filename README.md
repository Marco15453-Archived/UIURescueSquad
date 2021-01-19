# UIURescueSquad
 A new exiled plugin that add a new GOI to the game [FBI/UIU]

# Installation
Download the .dll file of the latest release and place it inside the Exiled Plugins folder

# Configs
```yaml
UIURescueSquad:
# Is the plugin enabled?
  is_enabled: true
  # How many mtfs respawns must have happened to spawn UIU
  respawns: 1
  # Probability of a UIU Squad replacing a MTF spawn
  probability: 50
  # Entrance broadcast announcement message (null to disable it)
  announcement_text: <b>The <color=#FFFA4B>UIU Rescue Squad</color> has arrived to the facility</b>
  # Entrance broadcast announcement message time
  announcement_time: 10
  # Disable NTF default Announce
  disable_n_t_f_announce: true
  # **ONLY WORKS IF DisableNTFAnnounce = true** Entrance Cassie Message
  announcement_cassie: The U I U Squad HasEntered AwaitingRecontainment {scpnum}
  announcment_cassie_no_scp: The U I U Squad HasEntered NoSCPsLeft
  # Use hints instead of broadcasts?
  use_hints_here: false
  # UIU Player broadcast (null to disable it)
  u_i_u_broadcast: <i>You are an</i><color=yellow><b> UIU trooper</b></color>, <i>help </i><color=#0377fc><b>MTFs</b></color><i> to finish its job</i>
  # UIU Player broadcast (null to disable it)
  u_i_u_broadcast_time: 10
  # UIU Soldier life (NTF CADET)
  u_i_u_soldier_life: 160
  # The items UIUs soldiers spawn with
  u_i_u_soldier_inventory:
  - KeycardNTFLieutenant
  - GunProject90
  - GunUSP
  - Disarmer
  - Medkit
  - Adrenaline
  - Radio
  - GrenadeFrag
  # Ammo UIUs soldiers spawn with. (5.56, 7.62, 9mm)
  u_i_u_soldier_ammo:
  - 80
  - 0
  - 100
  # UIU Soldier Rank (instead of Nine-Tailed Fox Cadet role)
  u_i_u_soldier_rank: UIU Soldier
  # UIU Agent life (NTF LIEUTENANT)
  u_i_u_agent_life: 175
  # The items UIUs agents spawn with
  u_i_u_agent_inventory:
  - KeycardNTFLieutenant
  - GunProject90
  - GunUSP
  - Disarmer
  - Medkit
  - Adrenaline
  - Radio
  - GrenadeFrag
  # Ammo UIUs agents spawn with (5.56, 7.62, 9mm)
  u_i_u_agent_ammo:
  - 80
  - 0
  - 100
  # UIU Agent Rank (instead of Nine-Tailed Fox Lieutenant role)
  u_i_u_agent_rank: UIU Agent
  # UIU Leader life (NTF COMMANDER)
  u_i_u_leader_life: 215
  # The items UIU leader spawn with.
  u_i_u_leader_inventory:
  - KeycardNTFLieutenant
  - GunProject90
  - GunUSP
  - Disarmer
  - Medkit
  - Adrenaline
  - Radio
  - GrenadeFrag
  # Ammo UIUs leader spawn with. (5.56, 7.62, 9mm)
  u_i_u_leader_ammo:
  - 80
  - 0
  - 100
  # UIU Leader Rank (instead of Nine-Tailed Fox Commander role)
  u_i_u_leader_rank: UIU Leader
```
