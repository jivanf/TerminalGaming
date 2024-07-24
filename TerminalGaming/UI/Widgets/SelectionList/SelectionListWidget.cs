using System;
using TerminalGaming.UI.Layouts.Vertical;

namespace TerminalGaming.UI.Widgets;

public class SelectionListWidget(SelectionListWidgetInput input)
    : VerticalLayout<SelectionListWidgetInput, SelectionListWidgetInitiator>(input)
{
    public override Type Script { get; } = typeof(SelectionListWidgetScript);
}
