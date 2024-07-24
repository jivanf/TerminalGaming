using System;
using TerminalGaming.Helpers;
using UnityEngine;

namespace TerminalGaming.Extensions;

public static class TerminalExtensions
{
    public static GameObject GetMainContainer(this Terminal terminal)
    {
        GameObject? mainContainer = terminal.GetContainer("MainContainer");

        if (mainContainer is null)
        {
            throw new Exception(
                "MainContainer couldn't be found. An update or another plugin may have changed the UI."
            );
        }

        return mainContainer;
    }

    public static GameObject GetGameContainer(this Terminal terminal)
    {
        return terminal.GetContainer("GameContainer") ?? terminal.InstantiateGameContainer();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public static GameObject? GetContainer(this Terminal terminal, string containerName)
    {
        return terminal.terminalUIScreen.gameObject.transform.Find(containerName)?.gameObject;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private static GameObject InstantiateGameContainer(this Terminal terminal)
    {
        GameObject container = ObjectHelpers.InstantiateWithoutChildren(
            terminal.GetContainer("MainContainer"),
            terminal.terminalUIScreen.transform
        );
        container.name = "GameContainer";

        const float reducedWidth = 70f;
        const float reducedHeight = 90f;

        var rectTransform = container.GetComponent<RectTransform>();
        Vector3 anchoredPosition3D = rectTransform.anchoredPosition3D;

        rectTransform.SetWidth(rectTransform.rect.width - reducedWidth);
        rectTransform.SetHeight(rectTransform.rect.height - reducedHeight);

        anchoredPosition3D += new Vector3(
            anchoredPosition3D.x + reducedWidth / 2,
            anchoredPosition3D.y + 20f,
            0
        );
        rectTransform.anchoredPosition3D = anchoredPosition3D;

        return container;
    }
}
