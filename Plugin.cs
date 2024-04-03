using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace PasteFromClipboard
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    public class PasteFromClipboardPlugin : BaseUnityPlugin

    {
        internal const string ModName = "PasteFromClipboard";
        internal const string ModVersion = "1.0.0";
        internal const string Author = "Azumatt";
        private const string ModGUID = Author + "." + ModName;
        private readonly Harmony _harmony = new(ModGUID);
        public static readonly ManualLogSource PasteFromClipboardLogger = BepInEx.Logging.Logger.CreateLogSource(ModName);

        private void Awake()
        {
            _harmony.PatchAll();
        }
    }
}