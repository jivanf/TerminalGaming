using System;
using TerminalGaming.UI.Renderables;
using TMPro;
using UnityEngine;

namespace TerminalGaming.UI.Elements;

public class StringLabelElementInstantiator : LabelElementInstantiator, IInstantiator<StringLabelElementInput>
{
    public GameObject Instantiate(StringLabelElementInput input, Type? script)
    {
        GameObject gameObject = base.Instantiate(input, script);

        gameObject.GetComponent<TextMeshProUGUI>().text = input.Value;

        return gameObject;
    }
}
