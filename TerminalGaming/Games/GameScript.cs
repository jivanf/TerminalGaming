using TerminalGaming.Extensions;
using TerminalGaming.Managers;
using UnityEngine;

namespace TerminalGaming.Games.Pong;

public abstract class GameScript : MonoBehaviour
{
    protected GameObject Container = null!;
    protected RectTransform ContainerRectTransform = null!;

    protected virtual void Start()
    {
        this.Container = TerminalManager.Instance.Terminal.GetGameContainer();
        this.ContainerRectTransform = this.Container.GetComponent<RectTransform>();
    }
}
