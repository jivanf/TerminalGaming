using TerminalGaming.UI.Renderables;
using UnityEngine;

namespace TerminalGaming.UI.Elements;

public class ElementRenderer : Renderer<ElementInput>
{
    public override void Render(ElementInput input, GameObject gameObject, RectTransform parent)
    {
        base.Render(input, gameObject, parent);

    }
}
