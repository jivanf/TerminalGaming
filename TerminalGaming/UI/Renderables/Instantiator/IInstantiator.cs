using System;
using UnityEngine;

namespace TerminalGaming.UI.Renderables;

public interface IInstantiator<in TInput>
    where TInput : RenderableInput
{
    public GameObject Instantiate(TInput input, Type? script);
}

public interface IInstantiator : IInstantiator<RenderableInput>;
