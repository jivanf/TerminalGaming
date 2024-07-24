using TerminalGaming.UI.Renderables;

namespace TerminalGaming.UI.Layouts;

public class LayoutInput : RenderableInput
{
    private const int DefaultSpacing = 0;

    public int Spacing { get; init; } = DefaultSpacing;
}
