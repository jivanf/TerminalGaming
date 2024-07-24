using TerminalGaming.UI.Renderables;
using UnityEngine;

namespace TerminalGaming.UI.Elements.Views.PlayerSelectionView;

public class TitleElement()
    : StringLabelElement(
        new StringLabelElementInput
        {
            Name = "TitleElement",
            Value = "PONG",
            FontSize = 128,
            CollapsedAnchorPosition =  new CollapsedAnchorPosition
            {
                Anchor = new Vector2(0.5f, 0.9f),
            },
        }
    );
