using System;
using TerminalGaming.UI.Renderables;
using TMPro;
using UnityEngine;

namespace TerminalGaming.UI.Elements;

public class IntLabelElementInstantiator : LabelElementInstantiator, IInstantiator<IntLabelElementInput>
{
    public GameObject Instantiate(IntLabelElementInput input, Type? script)
    {
        GameObject gameObject = base.Instantiate(input, script);

        gameObject.GetComponent<TextMeshProUGUI>().text = input.Value.ToString();

        return gameObject;
    }
}
