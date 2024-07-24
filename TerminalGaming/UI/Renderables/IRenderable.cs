using System;
using UnityEngine;

namespace TerminalGaming.UI.Renderables;

public interface IRenderable<in TInput> : IRenderable where TInput : RenderableInput
{
    public GameObject Instantiate(TInput input, Type? script);
}

public interface IRenderable : IDisposable
{
    public GameObject GameObject { get; }

    public RectTransform RectTransform { get; }

    public void Render(RectTransform? parent = null);
}
