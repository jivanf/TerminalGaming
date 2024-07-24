using System.Collections.Generic;
using TerminalGaming.UI.Layouts;

namespace TerminalGaming.UI.Widgets;

public class SelectionListWidgetInput : LayoutInput
{
    public required List<string> Options { get; init; }
}
