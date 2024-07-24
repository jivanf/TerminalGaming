using System;
using TerminalGaming.UI.Renderables;
using UnityEngine;
using UnityEngine.UI;

namespace TerminalGaming.UI.Elements;

public class RectInstantiator : Instantiator
{
    public override GameObject Instantiate(RenderableInput input, Type? script)
    {
        GameObject gameObject = base.Instantiate(input, script);
        gameObject.AddComponent<Image>();

        var image = gameObject.GetComponent<Image>();

        image.color = Color.green;

        return gameObject;
    }
}
