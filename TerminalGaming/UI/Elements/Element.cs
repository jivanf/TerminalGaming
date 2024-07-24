using TerminalGaming.Extensions;
using TerminalGaming.Managers;
using TerminalGaming.UI.Renderables;
using UnityEngine;
using Renderer = TerminalGaming.UI.Renderables.Renderer;

namespace TerminalGaming.UI.Elements;

public abstract class Element<TInput, TInstantiator, TRenderer>(TInput input)
    : Renderable<TInput, TInstantiator>(input)
    where TInput : ElementInput
    where TInstantiator : IInstantiator<TInput>, new()
    where TRenderer : IRenderer<TInput>, new()
{
    public override void Render(RectTransform? parent = null)
    {
        parent ??= TerminalManager
            .Instance.Terminal.GetGameContainer()
            .GetComponent<RectTransform>();

        new TRenderer().Render(this.Input, this.GameObject, parent);
    }
}

public abstract class Element<TInput, TInstantiator>(TInput input)
    : Element<TInput, TInstantiator, Renderer>(input)
    where TInput : ElementInput
    where TInstantiator : IInstantiator<TInput>, new();
