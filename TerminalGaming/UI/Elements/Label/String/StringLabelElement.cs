namespace TerminalGaming.UI.Elements;

public class StringLabelElement(StringLabelElementInput input)
    : LabelElement<StringLabelElementInput, StringLabelElementInstantiator>(input)
{
    public string Value
    {
        get => this.TextMesh.text;
        set => this.TextMesh.text = value;
    }
}
