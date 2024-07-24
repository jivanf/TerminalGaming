namespace TerminalGaming.UI.Elements;

public class IntLabelElement(IntLabelElementInput input) : LabelElement<IntLabelElementInput, IntLabelElementInstantiator>(input)
{
    public int Value
    {
        get => int.Parse(this.TextMesh.text ?? "0");
        set => this.TextMesh.text = value.ToString();
    }

    public void Increment(int increment = 1)
    {
        this.Value += increment;
    }

    public void Decrement(int decrement = 1)
    {
        this.Value -= decrement;
    }
}
