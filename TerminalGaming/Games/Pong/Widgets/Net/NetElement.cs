using TerminalGaming.UI.Elements;
using TerminalGaming.UI.Renderables;
using UnityEngine;

namespace TerminalGaming.Games.Pong.Widgets;

public class NetElement(int index)
    : RectElement(
        new RectElementInput
        {
            Name = "NetElement",
            Width = 2.5f,
            Height = 10,
            CollapsedAnchorPosition = new CollapsedAnchorPosition
            {
                Anchor = new Vector2(0.5f, 1),
                Offset = new Vector2(0, -(index * (NetGap + 10))),
            },
        }
    )
{
    private const float NetGap = 20;
}
