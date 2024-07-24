using System;
using System.Linq;
using TerminalGaming.UI.Renderables;
using TMPro;
using UnityEngine;

namespace TerminalGaming.UI.Elements;

public class LabelElementInstantiator : Instantiator<LabelElementInput>
{
    private const string FontAssetName = "3270-Regular SDF";

    public override GameObject Instantiate(LabelElementInput input, Type? script)
    {
        GameObject gameObject = base.Instantiate(input, script);

        var textMesh = gameObject.AddComponent<TextMeshProUGUI>();

        TMP_FontAsset fontAsset = Resources.FindObjectsOfTypeAll<TMP_FontAsset>().First(asset => asset.name == FontAssetName);

        textMesh.color = Color.green;
        textMesh.font = fontAsset;
        textMesh.fontSize = input.FontSize;

        return gameObject;
    }
}
