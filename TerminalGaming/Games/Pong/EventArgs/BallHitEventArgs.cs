using TerminalGaming.Games.Pong.Elements;

namespace TerminalGaming.Games.Pong.EventArgs;

public sealed class BallHitEventArgs : System.EventArgs
{
    public PaddleElement? HittingPaddle { get; set; }
}
