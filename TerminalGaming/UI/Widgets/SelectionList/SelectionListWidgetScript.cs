using BepInEx;
using BepInEx.Configuration;
using TerminalGaming.Games.Pong;
using TMPro;
using UnityEngine;

namespace TerminalGaming.UI.Widgets;

public class SelectionListWidgetScript : MonoBehaviour
{
    private const float SelectionCooldown = 0.2f;

    private TextMeshProUGUI[] options = null!;

    private int selectedOptionIndex;

    private float nextSelectionTime = 5f;

    private void Start()
    {
        this.options = this.gameObject.GetComponentsInChildren<TextMeshProUGUI>();

        this.SelectOption(0);
    }

    public void Update()
    {
        if (!(Time.time >= this.nextSelectionTime))
        {
            return;
        }

        if (new KeyboardShortcut(KeyCode.DownArrow).IsPressed())
        {
            this.SelectNextOption();

            this.nextSelectionTime = Time.time + SelectionCooldown;
        }

        if (new KeyboardShortcut(KeyCode.UpArrow).IsPressed())
        {
            this.SelectPreviousOption();

            this.nextSelectionTime = Time.time + SelectionCooldown;
        }

        if (UnityInput.Current.GetKeyDown(KeyCode.Return))
        {
            Router.Navigate<PongGameView>();

            this.nextSelectionTime = Time.time + SelectionCooldown;
        }
    }

    private void SelectOption(int index)
    {
        this.selectedOptionIndex = index;

        this.UpdateSelectedOptionText();
    }

    private void SelectNextOption()
    {
        this.DeselectOption();

        this.SelectOption((this.selectedOptionIndex + 1) % this.options.Length);
    }

    private void SelectPreviousOption()
    {
        this.DeselectOption();

        this.SelectOption(
            (this.selectedOptionIndex - 1 + this.options.Length) % this.options.Length
        );
    }

    private void UpdateSelectedOptionText()
    {
        this.options[this.selectedOptionIndex].text =
            $"> {this.options[this.selectedOptionIndex].text}";
    }

    private void DeselectOption()
    {
        this.options[this.selectedOptionIndex].text = this.options[
            this.selectedOptionIndex
        ].text.Replace("> ", "");
    }
}
