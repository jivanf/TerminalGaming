using TerminalGaming.UI;
using TerminalGaming.UI.Elements.Views.PlayerSelectionView;

namespace TerminalGaming.Games.Pong;

public class Game
{
    public void Render()
    {
        Router.Navigate<PlayerSelectionView>();
    }
}
