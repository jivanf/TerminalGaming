using System;
using System.Collections.ObjectModel;

namespace TerminalGaming.UI.Renderables;

public class InitData : IDisposable
{
    // TODO: Narrow collection types
    public ObservableCollection<IRenderable> Renderables { get; } = [];

    public virtual void AddRenderable(IRenderable renderable)
    {
        this.Renderables.Add(renderable);
    }

    /// <summary>
    /// Dispose every renderable and clear the collection.
    /// </summary>
    public virtual void Dispose()
    {
        foreach (IRenderable renderable in this.Renderables)
        {
            renderable.Dispose();
        }

        this.Renderables.Clear();
    }
}
