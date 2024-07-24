using BepInEx;
using LethalAPI.LibTerminal;
using LethalAPI.LibTerminal.Models;
using TerminalGaming.Commands;

namespace TerminalGaming
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private TerminalModRegistry commandRegistry = null!;

        private void Awake()
        {
            this.commandRegistry = TerminalRegistry.CreateTerminalRegistry();
            this.commandRegistry.RegisterFrom<PlayCommand>();
        }

        private void OnDestroy()
        {
            this.commandRegistry.Deregister();
        }
    }
}
