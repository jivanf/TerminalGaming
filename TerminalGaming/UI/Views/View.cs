using TerminalGaming.UI.Renderables;

namespace TerminalGaming.UI.Elements.Views;

public abstract class View<TInitiator>()
    : MultiRenderable<TInitiator>(new RenderableInput { FullScreen = true, }),
        IView
    where TInitiator : IInitiator, new();
