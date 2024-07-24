namespace TerminalGaming.UI.Renderables;

public static class Initiator<TInitiator, TInput>
    where TInitiator : IInitiator<TInput>, new()
    where TInput : RenderableInput
{
    private static readonly TInitiator Instance = new();

    public static InitData Init(TInput input) => Instance.Init(input);
}
