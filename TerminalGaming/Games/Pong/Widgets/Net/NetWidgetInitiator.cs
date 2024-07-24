using TerminalGaming.UI.Renderables;

namespace TerminalGaming.Games.Pong.Widgets;

public class NetWidgetInitiator : IInitiator
{
    public InitData Init(RenderableInput input)
    {
        var result = new InitData();

        for (int i = 0; i <= 12; i++)
        {
            result.AddRenderable(new NetElement(i));
        }

        return result;
    }
}
