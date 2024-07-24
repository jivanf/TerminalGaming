using TerminalGaming.Extensions;
using TerminalGaming.Managers;
using UnityEngine;

namespace TerminalGaming.UI.Renderables;

public abstract class MultiRenderable<TInput, TInitiator, TInstantiator, TRenderer>
    : Renderable<TInput, TInstantiator>,
        IMultiRenderable<TInput>
    where TInput : RenderableInput
    where TInitiator : IInitiator<TInput>, new()
    where TInstantiator : IInstantiator<TInput>, new()
    where TRenderer : IRenderer<TInput>, new()
{
    public InitData InitData { get; set; }

    public MultiRenderable(TInput input)
        : base(input)
    {
        this.InitData = this.Init();
    }

    public InitData Init()
    {
        return Initiator<TInitiator, TInput>.Init(this.Input);
    }

    public override void Render(RectTransform? parent = null)
    {
        parent ??= TerminalManager
            .Instance.Terminal.GetGameContainer()
            .GetComponent<RectTransform>();

        new TRenderer().Render(this.Input, this.GameObject, parent);

        foreach (IRenderable renderable in this.InitData.Renderables)
        {
            renderable.Render(this.GameObject.GetComponent<RectTransform>());
        }
    }

    /// <summary>
    /// Dispose the renderable and its children.
    /// </summary>
    public override void Dispose()
    {
        this.InitData.Dispose();

        base.Dispose();
    }
}

public abstract class MultiRenderable<TInput, TInitiator, TInstantiator>(TInput input)
    : MultiRenderable<TInput, TInitiator, TInstantiator, Renderer<TInput>>(input)
    where TInput : RenderableInput
    where TInitiator : IInitiator<TInput>, new()
    where TInstantiator : IInstantiator<TInput>, new();

public abstract class MultiRenderable<TInput, TInitiator>(TInput input)
    : MultiRenderable<TInput, TInitiator, Instantiator<TInput>, Renderer<TInput>>(input)
    where TInput : RenderableInput
    where TInitiator : IInitiator<TInput>, new();

public abstract class MultiRenderable<TInitiator>(RenderableInput? input = null)
    : MultiRenderable<RenderableInput, TInitiator, Instantiator, Renderer<RenderableInput>>(
        input ?? new RenderableInput()
    )
    where TInitiator : IInitiator, new();
