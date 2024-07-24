using System;
using TerminalGaming.Managers;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TerminalGaming.UI.Renderables;

public abstract class Renderable<TInput, TInstantiator> : IRenderable<TInput>
    where TInput : RenderableInput
    where TInstantiator : IInstantiator<TInput>, new()
{
    public TInput Input { get; }

    public GameObject GameObject { get; }

    public RectTransform RectTransform { get; }

    public virtual Type? Script { get; }

    protected Renderable(TInput input)
    {
        this.Input = input;

        // The script must be initialized in the property declaration, which makes it accessible here
        // ReSharper disable once VirtualMemberCallInConstructor
        this.GameObject = this.Instantiate(input, this.Script);

        // The instantiator (called in the previous line) ensures the game object has a `RectTransform` component
        this.RectTransform = this.GameObject.GetComponent<RectTransform>();

        UIManager.Instance.AddRenderable(this);
    }

    public GameObject Instantiate(TInput input, Type? script)
    {
        return new TInstantiator().Instantiate(input, script);
    }

    public abstract void Render(RectTransform? parent = null);

    public virtual void Dispose()
    {
        UIManager.Instance.RemoveRenderable(this);

        Object.Destroy(this.GameObject);
    }
}
