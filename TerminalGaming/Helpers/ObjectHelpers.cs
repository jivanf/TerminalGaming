using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TerminalGaming.Helpers;

public static class ObjectHelpers
{
    public static GameObject InstantiateWithoutChildren(GameObject? original, Transform parent)
    {
        GameObject? gameObject = Object.Instantiate(original, parent);

        if (gameObject is null)
        {
            throw new Exception("Object couldn't be instantiated.");
        }

        foreach (Transform child in gameObject.transform)
        {
            Object.Destroy(child.gameObject);
        }

        return gameObject;
    }
}
