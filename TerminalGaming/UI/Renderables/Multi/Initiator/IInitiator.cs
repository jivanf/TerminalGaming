namespace TerminalGaming.UI.Renderables;

public interface IInitiator<in TInput> where TInput : RenderableInput
{
    public InitData Init(TInput input);
}

public interface IInitiator : IInitiator<RenderableInput>;
