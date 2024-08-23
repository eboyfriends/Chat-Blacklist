using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Entities;
using Microsoft.Extensions.Logging;

namespace Chat_Blacklist {

    public class Main : BasePlugin, IPluginConfig<MainConfig>
    {
        public override string ModuleName => "eboyfriends";
        public override string ModuleVersion => "6.6.6";
        public override string ModuleAuthor => "eboyfriends";
        public required MainConfig Config { get; set; }

        public override void Load(bool hotReload)
        {
            Logger.LogInformation("We are loading Chat_Blacklist!");
            
            AddCommandListener("say", OnPlayerSay);
            AddCommandListener("say_team", OnPlayerSay);
        }

        public override void Unload(bool hotReload)
        {
            Logger.LogInformation("We are unloading Chat_Blacklist!");

            RemoveCommandListener("say", OnPlayerSay, HookMode.Pre);
            RemoveCommandListener("say_team", OnPlayerSay, HookMode.Pre);
        }

        public void OnConfigParsed(MainConfig config) {
            if (config == null) {
                Logger.LogError("Parsed config is null in OnConfigParsed method");
                return;
            }
            
            Config = config;
            Logger.LogInformation("Config successfully parsed and set");
        }

        private HookResult OnPlayerSay(CCSPlayerController? player, CommandInfo info) {
            if (player == null || player.IsBot || Config.Blacklisted == null) {
                return HookResult.Continue;
            }
            string message = info.GetArg(1);

            foreach (var Word in Config.Blacklisted) {
                if (!string.IsNullOrEmpty(Word) && message.Contains(Word, StringComparison.OrdinalIgnoreCase)) {
                    
                    // do this incase its a command.
                    player.ExecuteClientCommandFromServer($"css_{message.Substring(1)}");
                    return HookResult.Handled;
                }
            }

            return HookResult.Continue;
        }
    }
}

