using TerminalGaming.UI.Renderables;
using UnityEngine.UI;

namespace TerminalGaming.UI.Layouts.Vertical;

public abstract class VerticalLayout<TInput, TInitiator>(TInput input)
    : Layout<VerticalLayoutGroup, TInput, TInitiator>(input)
    where TInitiator : IInitiator<TInput>, new()
    where TInput : LayoutInput;

public abstract class VerticalLayout<TInitiator>(LayoutInput? input = null)
    : VerticalLayout<LayoutInput, TInitiator>(input ?? new LayoutInput())
    where TInitiator : IInitiator<LayoutInput>, new();
