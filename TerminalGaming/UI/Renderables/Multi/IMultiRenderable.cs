namespace TerminalGaming.UI.Renderables;

public interface IMultiRenderable : IRenderable
{
    public InitData InitData { get; set; }

    /// <summary>
    /// Return the single- and multi-renderables to be rendered.
    /// </summary>
    public InitData Init();
};

public interface IMultiRenderable<in TInput> : IRenderable<TInput>, IMultiRenderable
    where TInput : RenderableInput;
