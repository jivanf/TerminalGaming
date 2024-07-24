using TerminalGaming.UI.Elements;
using TerminalGaming.UI.Renderables;
using UnityEngine;

namespace TerminalGaming.Games.Pong.Elements;

public enum ScorePosition
{
    Left,
    Right,
}

public class ScoreElement(ScorePosition position)
    : IntLabelElement(
        new IntLabelElementInput
        {
            Name = ScorePosition.Left == position ? "LeftScoreElement" : "RightScoreElement",
            CollapsedAnchorPosition = new CollapsedAnchorPosition
            {
                Anchor = new Vector2(position == ScorePosition.Left ? 0.25f : 0.75f, 0.9f),
            },
        }
    );
