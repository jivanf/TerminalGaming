using System;
using TerminalGaming.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace TerminalGaming.UI.Renderables;

// TODO: Create `RectTransformInstantiator`
public class Instantiator<TInput> : IInstantiator<TInput>
    where TInput : RenderableInput
{
    public virtual GameObject Instantiate(TInput input, Type? script)
    {
        var gameObject = new GameObject(input.Name, [typeof(RectTransform)]);

        if (script != null)
        {
            gameObject.AddComponent(script);
        }

        if (input.Debug && input is not LabelElementInput)
        {
            var image = gameObject.AddComponent<Image>();
            image.color = Color.white;
        }

        return gameObject;
    }
}

public class Instantiator : Instantiator<RenderableInput>;
