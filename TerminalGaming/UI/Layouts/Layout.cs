using TerminalGaming.UI.Renderables;
using UnityEngine.UI;

namespace TerminalGaming.UI.Layouts;

public abstract class Layout<TLayoutGroup, TInput, TInitiator, TInstantiator, TRenderer>(TInput input)
    : MultiRenderable<TInput, TInitiator, LayoutInstantiator<TLayoutGroup>>(input)
    where TLayoutGroup : HorizontalOrVerticalLayoutGroup
    where TInitiator : IInitiator<TInput>, new()
    where TInstantiator : IInstantiator<TInput>, new()
    where TRenderer : IRenderer<TInput>, new()
    where TInput : LayoutInput;

public abstract class Layout<TLayoutGroup, TInput, TInitiator>(TInput input)
    : MultiRenderable<TInput, TInitiator, LayoutInstantiator<TLayoutGroup>>(input)
    where TLayoutGroup : HorizontalOrVerticalLayoutGroup
    where TInitiator : IInitiator<TInput>, new()
    where TInput : LayoutInput;

public abstract class Layout<TLayoutGroup, TInitiator>()
    : Layout<TLayoutGroup, LayoutInput, TInitiator>(new LayoutInput())
    where TLayoutGroup : HorizontalOrVerticalLayoutGroup
    where TInitiator : IInitiator<LayoutInput>, new();
