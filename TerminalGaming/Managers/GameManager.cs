using BepInEx.Configuration;
using TerminalGaming.Commands;
using TerminalGaming.Extensions;
using TerminalGaming.Games.Pong;
using TMPro;
using UnityEngine;

namespace TerminalGaming.Managers;

public class GameManager : Manager<GameManager>
{
    public delegate void QuitEventHandler();
    public event QuitEventHandler Quit = null!;

    public void Awake()
    {
        this.Quit += this.GameScript_Quit;
    }

    public TGame AddGame<TGame>()
        where TGame : GameScript
    {
        var game = this.gameObject.AddComponent<TGame>();

        return game;
    }

    private void OnQuit()
    {
        this.Quit.Invoke();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void GameScript_Quit()
    {
        Terminal terminal = Manager<TerminalManager>.Instance.Terminal;
        GameObject mainContainer = terminal.GetMainContainer();

        PlayCommand.SetMainContainerActive(true);

        // Input field is deactivated (loses focus) when the main container is deactivated.
        var inputField = mainContainer
            .transform.Find("Scroll View/Viewport/InputField (TMP)")
            .GetComponent<TMP_InputField>();
        inputField.ActivateInputField();
    }

    protected void Update()
    {
        if (new KeyboardShortcut(KeyCode.Delete).IsDown())
        {
            this.OnQuit();

            Destroy(this.gameObject);
        }
    }
}
