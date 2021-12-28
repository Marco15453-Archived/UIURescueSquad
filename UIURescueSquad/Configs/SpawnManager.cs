namespace UIURescueSquad.Configs
{
    using Exiled.API.Features;
    using System.ComponentModel;
    using UnityEngine;

    public class SpawnManager
    {
        [Description("The chance for UIU Rescue Squad to spawn instead of MTF.")]
        public int SpawnChance { get; set; } = 50;

        [Description("The maximum size of a UIU Rescue Squad.")]
        public uint MaxSquad { get; set; } = 8;

        [Description("How many respawn waves must occur before considering UIU Rescue Squad to spawn.")]
        public int RespawnDelay { get; set; } = 1;

        [Description("The maximum number of times UIU Rescue Squad can spawn per game.")]
        public int MaxSpawns { get; set; } = 1;

        [Description("The message annouced by CASSIE when UIU Rescue Squad spawns.")]
        public string EntryAnnoucement { get; set; } = "The U I U Squad designated {designation} HasEntered AwaitingRecontainment {scpnum}";

        [Description("The message annouced by CASSIE when UIU Rescue Squad spawns when no scps are alive.")]
        public string EntryAnnoucementNoScp { get; set; } = "The U I U Squad designated {designation} HasEntered NoSCPsLeft";

        [Description("The broadcast sent to UIU Rescue Squad when they spawn.")]
        public Broadcast SpawnBroadcast { get; set; } = new Broadcast("<i>You are an</i><color=yellow><b> UIU trooper</b></color>, <i>help </i><color=#0377fc><b>MTFs</b></color><i> to finish its job</i>");

        [Description("The broadcast shown when the UIU Rescue Squad spawns.")]
        public Broadcast EntryBroadcast { get; set; } = new Broadcast("<color=orange>UIU Rescue Squad has entered the facility!</color>");

        [Description("UIU Rescue Squad spawn position.")]
        public Vector3 SpawnPos { get; set; } = new Vector3(170f, 985f, 29f);
    }
}
