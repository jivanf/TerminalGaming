using TerminalGaming.Extensions;
using UnityEngine;

namespace TerminalGaming.UI.Renderables;

public class Renderer<TInput> : IRenderer<TInput>
    where TInput : RenderableInput
{
    public virtual void Render(TInput input, GameObject gameObject, RectTransform parent)
    {
        var rectTransform = gameObject.GetComponent<RectTransform>();

        rectTransform.SetParent(parent);

        rectTransform.localScale = Vector3.one;
        rectTransform.eulerAngles = new Vector3(350, 143, 0);
        rectTransform.localEulerAngles = Vector3.zero;
        rectTransform.localPosition = Vector3.zero;

        switch (input)
        {
            case { FullScreen: { } fullScreen }:
                if (!fullScreen)
                {
                    break;
                }

                rectTransform.anchorMin = Vector2.zero;
                rectTransform.anchorMax = Vector2.one;

                rectTransform.offsetMin = rectTransform.offsetMax = Vector2.zero;

                break;

            case { AnchorPosition: { } anchorPosition }:
                rectTransform.anchorMin = anchorPosition.AnchorMin;
                rectTransform.anchorMax = anchorPosition.AnchorMax;

                if (input is { AnchorPosition.Offset: { } anchorOffset, })
                {
                    rectTransform.offsetMin = rectTransform.offsetMax = anchorOffset;
                }
                else
                {
                    // Remove default offsets. This ensures the size is calculated using the given size and position settings.
                    rectTransform.offsetMin = rectTransform.offsetMax = Vector2.zero;
                }

                break;
            case { CollapsedAnchorPosition: { } offsetPosition }:
                rectTransform.anchorMin = rectTransform.anchorMax = offsetPosition.Anchor;

                if (input is { CollapsedAnchorPosition.Offset: { } collapsedAnchorOffset, })
                {
                    rectTransform.offsetMin = rectTransform.offsetMax = collapsedAnchorOffset;
                }
                else
                {
                    // Remove default offsets. This ensures the size is calculated using the given size and position settings.
                    rectTransform.offsetMin = rectTransform.offsetMax = Vector2.zero;
                }
                break;
            case { Position: { } position }:
                rectTransform.anchoredPosition3D = position;

                break;
        }

        // Set -1 as the z position to ensure that the element is rendered on top of the terminal screen. With this,
        // derived classes don't need to set the z position and can use `anchoredPosition` instead.
        rectTransform.anchoredPosition3D = new Vector3(
            rectTransform.anchoredPosition.x,
            rectTransform.anchoredPosition.y,
            -1
        );

        if (input is { Width: { } width, Height: { } height })
        {
            rectTransform.SetSize(width, height);
        }
    }
}

public class Renderer : Renderer<RenderableInput>;
