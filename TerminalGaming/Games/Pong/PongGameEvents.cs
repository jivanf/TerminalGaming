using TerminalGaming.Games.Pong.EventArgs;

namespace TerminalGaming.Games.Pong;

public static class PongGameEvents
{
    public delegate void RallyStartEventHandler(RallyStartEventArgs args);
    public static event RallyStartEventHandler RallyStart = null!;

    public delegate void BallHitEventHandler(BallHitEventArgs args);
    public static event BallHitEventHandler BallHit = null!;

    public static void OnRallyStart(RallyStartEventArgs args)
    {
        RallyStart.Invoke(args);
    }

    public static void OnBallHit(BallHitEventArgs args)
    {
        BallHit.Invoke(args);
    }
}
