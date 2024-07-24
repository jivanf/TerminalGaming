using System;
using UnityEngine;

namespace TerminalGaming.Extensions;

public enum BorderPosition
{
    Top,
    Bottom,
    Left,
    Right
}

/// <see href="https://forum.unity.com/threads/modify-the-width-and-height-of-recttransform.270993/#post-7950487" />
public static class RectTransformExtensions
{
    public static Vector2 GetSize(this RectTransform source) => source.rect.size;

    public static float GetWidth(this RectTransform source) => source.rect.size.x;

    public static float GetHeight(this RectTransform source) => source.rect.size.y;

    /// <summary>
    /// Sets the sources RT size to the same as the toCopy's RT size.
    /// </summary>
    public static void SetSize(this RectTransform source, RectTransform toCopy)
    {
        source.SetSize(toCopy.GetSize());
    }

    /// <summary>
    /// Sets the sources RT size to the same as the newSize.
    /// </summary>
    public static void SetSize(this RectTransform source, Vector2 newSize)
    {
        source.SetSize(newSize.x, newSize.y);
    }

    /// <summary>
    /// Sets the sources RT size to the new width and height.
    /// </summary>
    public static void SetSize(this RectTransform source, float width, float height)
    {
        source.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        source.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    public static void SetWidth(this RectTransform source, float width)
    {
        source.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    public static void SetHeight(this RectTransform source, float height)
    {
        source.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
    }

    public static Vector2 GetLocalBorderPosition(
        this RectTransform source,
        BorderPosition borderPosition
    )
    {
        Vector2 sourcePosition = source.localPosition;

        float sourceMiddleWidth = source.GetWidth() / 2;
        float sourceMiddleHeight = source.GetHeight() / 2;

        // The pivot position of the `RectTransform` is in the center. By adding or subtracting half
        // of the `RectTransform`'s width or height, we get the position of the `RectTransform`'s border.
        return borderPosition switch
        {
            BorderPosition.Top => sourcePosition + new Vector2(0, sourceMiddleHeight),
            BorderPosition.Bottom => sourcePosition - new Vector2(0, sourceMiddleHeight),
            BorderPosition.Right => sourcePosition + new Vector2(sourceMiddleWidth, 0),
            BorderPosition.Left => sourcePosition - new Vector2(sourceMiddleWidth, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(borderPosition), borderPosition, null)
        };
    }

    public static bool CollidesWith(this RectTransform source, RectTransform collider)
    {
        Vector2 colliderPosition = collider.localPosition;
        
        (float, float) colliderXBoundaries = (
            collider.GetLocalBorderPosition(BorderPosition.Right).x,
            collider.GetLocalBorderPosition(BorderPosition.Left).x
        );
        
        (float, float) colliderYBoundaries = (
            collider.GetLocalBorderPosition(BorderPosition.Top).y,
            collider.GetLocalBorderPosition(BorderPosition.Bottom).y
        );
        
        // (float, float) colliderXBoundaries = (
        //     colliderPosition.x + collider.GetWidth() / 2,
        //     colliderPosition.x - collider.GetWidth() / 2
        // );
        //
        // (float, float) colliderYBoundaries = (
        //     colliderPosition.y + collider.GetHeight() / 2,
        //     colliderPosition.y - collider.GetHeight() / 2
        // );

        Vector3 localPosition = source.localPosition;

        return localPosition.x + source.GetWidth() / 2 > colliderXBoundaries.Item2
            && localPosition.x - source.GetWidth() / 2 < colliderXBoundaries.Item1
            && localPosition.y + source.GetHeight() / 2 > colliderYBoundaries.Item2
            && localPosition.y - source.GetHeight() / 2 < colliderYBoundaries.Item1;
    }
}
