using UnityEngine;

namespace TerminalGaming.Extensions;

public static class GameObjectExtensions
{
    public static bool HasComponent<TComponent>(this GameObject gameObject)
    {
        return gameObject.GetComponent<TComponent>() != null;
    }
}
