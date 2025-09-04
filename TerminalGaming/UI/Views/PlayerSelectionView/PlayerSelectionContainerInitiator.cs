using TerminalGaming.UI.Renderables;
using TerminalGaming.UI.Widgets;

namespace TerminalGaming.UI.Elements.Views.PlayerSelectionView;

public class PlayerSelectionContainerInitiator : IInitiator
{
    public InitData Init(RenderableInput input)
    {
        var data = new InitData();

        data.AddRenderable(new TitleElement());

        data.AddRenderable(
            new SelectionListWidget(
                new SelectionListWidgetInput
                {
                    Options = ["1 PLAYER"],
                    Width = 500,
                    Height = 300,
                    Spacing = 16,
                }
            )
        );

        return data;
    }
}
