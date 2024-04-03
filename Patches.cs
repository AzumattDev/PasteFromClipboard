using System.Linq;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace PasteFromClipboard;

[HarmonyPatch(typeof(PlayerCustomizer), nameof(PlayerCustomizer.RunTerminal))]
static class PlayerCustomizerUpdatePatch
{
    static void Postfix(PlayerCustomizer __instance)
    {
        // If the player is ctrl+v'ing, then we want to paste the clipboard into the terminal
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V))
        {
            string clipboardText = GUIUtility.systemCopyBuffer;

            // Ensure the font supports the characters in the clipboard text
            // Paste the text from the clipboard*/
            __instance.faceText.text = clipboardText;
        }
    }
}