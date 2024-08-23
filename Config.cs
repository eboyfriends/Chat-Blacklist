using CounterStrikeSharp.API.Core;

namespace Chat_Blacklist {
    public class MainConfig : BasePluginConfig {
        public List<string> Blacklisted { get; set; } = new List<string> 
        { 
            "!1",
            "!2",
            "!3",
            "!4",
            "!5",
            "!6",
            "!7",
            "!8",
            "!9",
            "!ws",
            "!wp",
            "!fov",
            "!streak"
        };
    }
}

