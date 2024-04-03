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
            if (EnsureFontSupport(__instance.faceText, clipboardText))
            {
                // Paste the text from the clipboard
                __instance.faceText.text = clipboardText;
            }
        }
    }

    static bool EnsureFontSupport(TextMeshProUGUI textMesh, string text)
    {
        if (FontSupportsCharacters(textMesh.font, text)) return true;
        PasteFromClipboardPlugin.PasteFromClipboardLogger.LogWarning($"Font does not support characters in clipboard text. Hopefully more characters are supported in the future.");
        return false;
    }


    static bool FontSupportsCharacters(TMP_FontAsset font, string text)
    {
        return text.All(c => font.HasCharacter(c) || char.IsControl(c));
    }
}