using UnityEngine;

namespace TerminalGaming.UI.Renderables;

public interface IRenderer<in TInput>
    where TInput : RenderableInput
{
    public void Render(TInput input, GameObject gameObject, RectTransform parent);
}

public interface IRenderer : IRenderer<RenderableInput>;
