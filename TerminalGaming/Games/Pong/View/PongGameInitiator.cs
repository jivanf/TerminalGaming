using TerminalGaming.Games.Pong.Elements;
using TerminalGaming.Games.Pong.Widgets;
using TerminalGaming.UI.Renderables;

namespace TerminalGaming.Games.Pong;

public class PongGameInitiator : IInitiator
{
    public InitData Init(RenderableInput input)
    {
        var data = new InitData();

        // Paddles
        data.AddRenderable(new PaddleElement(PaddlePosition.Left));
        data.AddRenderable(new PaddleElement(PaddlePosition.Right));

        // Ball
        data.AddRenderable(new BallElement());

        // Scores
        data.AddRenderable(new ScoreElement(ScorePosition.Left));
        data.AddRenderable(new ScoreElement(ScorePosition.Right));

        // Net
        data.AddRenderable(new NetWidget());

        return data;
    }
}
