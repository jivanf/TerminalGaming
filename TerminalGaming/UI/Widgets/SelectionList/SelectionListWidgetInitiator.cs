using System.Linq;
using TerminalGaming.UI.Elements;
using TerminalGaming.UI.Renderables;

namespace TerminalGaming.UI.Widgets;

public class SelectionListWidgetInitiator : IInitiator<SelectionListWidgetInput>
{
    public InitData Init(SelectionListWidgetInput input)
    {
        var data = new InitData();

        foreach (
            StringLabelElement optionElement in input.Options.Select(
                option =>
                    new StringLabelElement(
                        new StringLabelElementInput
                        {
                            Name = "SelectionListOptionElement",
                            Value = option,
                        }
                    )
            )
        )
        {
            data.AddRenderable(optionElement);
        }

        return data;
    }
}
