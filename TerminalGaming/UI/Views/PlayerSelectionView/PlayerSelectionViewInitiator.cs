using TerminalGaming.UI.Renderables;

namespace TerminalGaming.UI.Elements.Views.PlayerSelectionView;

public class PlayerSelectionViewInitiator : IInitiator
{
    public InitData Init(RenderableInput input)
    {
        var data = new InitData();

        data.AddRenderable(new PlayerSelectionContainer());

        return data;
    }
}
