using System;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

namespace TerminalGaming.UI.Renderables;

public interface IRenderableInput;

public class RenderableInput : IRenderableInput
{
    private string? name;
    private string? derivedClassName;

    public string Name
    {
        get
        {
            if (this.name != null)
            {
                return this.name;
            }

            if (this.derivedClassName != null)
            {
                return this.derivedClassName;
            }

            // There may be concrete classes before we reach the target class, such as the instantiator class. By
            // starting from `Renderable`, we skip these classes.
            var frames = new StackTrace()
                .GetFrames()!
                .SkipWhile(
                    frame =>
                        frame.GetMethod()!.DeclaringType!.GetInterface(nameof(IRenderable)) == null
                )
                .Skip(1);

            foreach (StackFrame frame in frames)
            {
                Type? type = frame?.GetMethod()?.DeclaringType;

                if (type is not { IsAbstract: false })
                {
                    continue;
                }

                this.derivedClassName = type.Name.Replace("Input", "");

                break;
            }

            // Fallback if no concrete type found
            this.derivedClassName ??= this.GetType().Name.Replace("Input", "");

            return this.derivedClassName;
        }
        set => this.name = value;
    }

    public float? Width { get; init; }

    public float? Height { get; init; }

    public AnchorPosition? AnchorPosition { get; init; }

    public CollapsedAnchorPosition? CollapsedAnchorPosition { get; init; }

    /// <summary>
    /// Makes the transform span the entire terminal screen. This is equivalent to setting AnchorMin to zero and
    /// <c>AnchorMax</c> to one, and it overrides any other size and anchor position settings.
    /// </summary>
    public bool? FullScreen { get; init; }

    public Vector2? Position { get; init; }

    public virtual bool Debug { get; init; } = false;
}

public class AnchorPosition
{
    public Vector2 AnchorMin { get; init; }

    public Vector2 AnchorMax { get; init; }

    public Vector2? Offset { get; init; }
}

public class CollapsedAnchorPosition
{
    public Vector2 Anchor { get; init; }

    public Vector2? AnchoredPosition { get; init; }

    public Vector2? SizeDelta { get; init; }

    public Vector2? Offset { get; init; }
}
