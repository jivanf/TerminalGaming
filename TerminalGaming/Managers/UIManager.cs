using System.Collections.Generic;
using System.Linq;
using TerminalGaming.UI.Renderables;
using UnityEngine;

namespace TerminalGaming.Managers;

public class UIManager : Manager<UIManager>
{
    public Dictionary<IRenderable, GameObject> Renderables { get; set; } = new();

    public void AddRenderable(IRenderable renderable)
    {
        this.Renderables.Add(renderable, renderable.GameObject);
    }

    public TRenderable GetRenderable<TRenderable>(string? renderableName = null)
        where TRenderable : class, IRenderable
    {
        return this.Renderables.First(
                r =>
                    renderableName is null
                        ? r.Key is TRenderable
                        : r.Key is TRenderable && r.Value.name == renderableName
            ).Key as TRenderable;
    }

    public void RemoveRenderable(IRenderable renderable)
    {
        this.Renderables.Remove(renderable);
    }
}
