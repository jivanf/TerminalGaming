using TerminalGaming.UI.Renderables;
using UnityEngine;

namespace TerminalGaming.Games.Pong.Widgets;

public class NetWidget()
    : MultiRenderable<NetWidgetInitiator>(
        new RenderableInput
        {
            CollapsedAnchorPosition = new CollapsedAnchorPosition
            {
                Anchor = new Vector2(0.5f, 0.95f),
            },
        }
    );
