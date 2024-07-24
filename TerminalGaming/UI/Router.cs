using TerminalGaming.UI.Elements.Views;

namespace TerminalGaming.UI;

public static class Router
{
    private static IView? activeView;

    public static void Navigate<TView>() where TView : IView, new()
    {
        activeView?.Dispose();

        activeView = new TView();

        activeView.Render();
    }
}
