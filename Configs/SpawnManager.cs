using System.ComponentModel;

namespace UIURescueSquad.Configs
{ 
    public class SpawnManager
    {

        [Description("How many respawn waves must occur before considering UIU to spawn.")]
        public int Respawns { get; private set; } = 1;

        [Description("The maximum number of times UIU Rescue Squad can spawn per game.")]
        public int MaxSpawns { get; set; } = 3;

        [Description("Probability of a UIU Squad replacing a MTF spawn")]
        public int Probability { get; private set; } = 50;

        [Description("The maximum size of a UIU squad")]
        public int MaxSquad { get; private set; } = 8;

        [Description("UIU entrance Cassie Message")]
        public string UiuAnnouncementCassie { get; private set; } = "The U I U Squad designated {designation} HasEntered AllRemaining AwaitingRecontainment {scpnum}";
        public string UiuAnnouncmentCassieNoScp { get; private set; } = "The U I U Squad designated {designation} HasEntered AllRemaining NoSCPsLeft";

        [Description("NTF entrance Cassie Message (leave empty to use default NTF cassie entrance)")]
        public string NtfAnnouncementCassie { get; private set; } = "MTFUnit epsilon 11 designated {designation} hasentered AllRemaining AwaitingRecontainment {scpnum}";
        public string NtfAnnouncmentCassieNoScp { get; private set; } = "MTFUnit epsilon 11 designated {designation} hasentered AllRemaining NoSCPsLeft";

        [Description("Cassie Subtitles")]
        public bool Subtitles { get; private set; } = false;
    }
}
