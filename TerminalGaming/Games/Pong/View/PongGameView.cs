using System;
using TerminalGaming.UI.Elements.Views;

namespace TerminalGaming.Games.Pong;

public class PongGameView : View<PongGameInitiator>
{
    public override Type Script { get; } = typeof(PongGameScript);
}
