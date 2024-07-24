using TerminalGaming.UI.Renderables;
using TMPro;

namespace TerminalGaming.UI.Elements;

public abstract class LabelElement<TInput, TInstantiator>
    : Element<TInput, TInstantiator>
    where TInstantiator : IInstantiator<TInput>, new()
    where TInput : LabelElementInput
{
    protected TextMeshProUGUI TextMesh { get; private set; }

    protected LabelElement(TInput input) : base(input)
    {
        // The instantiator ensures the game object has a `TextMeshProUGUI` component
        this.TextMesh = this.GameObject.GetComponent<TextMeshProUGUI>();
    }
}

public abstract class LabelElement<TInput>(TInput input)
    : LabelElement<TInput, LabelElementInstantiator>(input)
    where TInput : LabelElementInput;