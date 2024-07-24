using TerminalGaming.UI.Elements;
using TerminalGaming.UI.Renderables;
using UnityEngine;

namespace TerminalGaming.Games.Pong.Elements;

public class BallElement()
    : RectElement(
        new RectElementInput
        {
            Name = "BallElement",
            Width = 10,
            Height = 10,
            CollapsedAnchorPosition = new CollapsedAnchorPosition
            {
                Anchor = new Vector2(0.5f, 0.5f),
            },
        }
    );
