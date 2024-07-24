using System;
using LethalAPI.LibTerminal.Attributes;
using LethalAPI.LibTerminal.Models;
using TerminalGaming.Enums;
using TerminalGaming.Extensions;
using TerminalGaming.Managers;
using TerminalGaming.UI;
using TerminalGaming.UI.Elements.Views.PlayerSelectionView;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TerminalGaming.Commands;

public class PlayCommand
{
    [TerminalCommand("Play")]
    public string? Play(ArgumentStream input, Terminal commandTerminal)
    {
        input.TryReadNext(out string gameNameInput);

        if (!Enum.TryParse(gameNameInput, true, out GameName gameName))
        {
            return null;
        }

        Manager<TerminalManager>.Instance.Terminal = commandTerminal;

        // TODO: Add game in `PlayerSelectionView`
        GameManager gameManager = Manager<GameManager>.Instance;

        SetMainContainerActive(false);

        Router.Navigate<PlayerSelectionView>();

        return commandTerminal.currentText;
    }

    public static void SetMainContainerActive(bool value)
    {
        Terminal terminal = Manager<TerminalManager>.Instance.Terminal;

        GameObject mainContainer = terminal.GetMainContainer();
        GameObject gameContainer = terminal.GetGameContainer();

        if (value)
        {
            mainContainer.SetActive(true);
            Object.Destroy(gameContainer);
        }
        else
        {
            gameContainer.SetActive(true);
            mainContainer.SetActive(false);
        }
    }
}
