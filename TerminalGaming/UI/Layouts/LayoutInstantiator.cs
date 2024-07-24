using System;
using TerminalGaming.UI.Renderables;
using UnityEngine;
using UnityEngine.UI;

namespace TerminalGaming.UI.Layouts;

public class LayoutInstantiator<TLayoutGroup> : IInstantiator<LayoutInput>
    where TLayoutGroup : HorizontalOrVerticalLayoutGroup
{
    public GameObject Instantiate(LayoutInput input, Type? script)
    {
        var gameObject = new GameObject(input.Name);
        var layoutGroup = gameObject.AddComponent<TLayoutGroup>();

        if (script != null)
        {
            gameObject.AddComponent(script);
        }

        // Default layout configuration
        layoutGroup.childForceExpandHeight = false;

        // Input layout configuration
        layoutGroup.spacing = input.Spacing;

        return gameObject;
    }
}
