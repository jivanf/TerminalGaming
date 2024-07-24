using TerminalGaming.Games.Pong.Elements;

namespace TerminalGaming.Games.Pong.EventArgs;

public sealed class RallyStartEventArgs : System.EventArgs
{
    public ScoreElement? WinnerScore { get; set; }
}
