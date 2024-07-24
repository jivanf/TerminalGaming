using TerminalGaming.UI.Elements;
using TerminalGaming.UI.Renderables;
using UnityEngine;

namespace TerminalGaming.Games.Pong.Elements;

public enum PaddlePosition
{
    Left,
    Right,
}

public class PaddleElement(PaddlePosition position)
    : RectElement(
        new RectElementInput
        {
            Name = PaddlePosition.Left == position ? "LeftPaddleElement" : "RightPaddleElement",
            Width = PaddleWidth,
            Height = PaddleHeight,
            CollapsedAnchorPosition = new CollapsedAnchorPosition
            {
                Anchor = new Vector2(position == PaddlePosition.Left ? 0 : 1, 0.5f),
                AnchoredPosition = new Vector2(
                    position == PaddlePosition.Left ? -PaddleWidth / 2 : PaddleHeight / 2,
                    0
                ),
            },
        }
    )
{
    private const float PaddleWidth = 10;
    private const float PaddleHeight = 50;

    public bool IsHittingBall = false;
}
