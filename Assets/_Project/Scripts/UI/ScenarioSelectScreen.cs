using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioSelectScreen : UIScreen
{
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private Transform buttonParent;
    [SerializeField] private GameObject loadingIndicator;

    private readonly List<Button> buttons = new List<Button>();

    public void Build(IReadOnlyList<ScenarioData> scenarios, Action<ScenarioData> onPicked)
    {
        if (buttons.Count > 0)
        {
            return;
        }

        foreach (ScenarioData scenario in scenarios)
        {
            Button button = Instantiate(buttonPrefab, buttonParent);
            button.GetComponentInChildren<TMP_Text>().text = scenario.DisplayName;
            button.onClick.AddListener(() => onPicked(scenario));
            buttons.Add(button);
        }
    }

    public void SetLoading(bool isLoading)
    {
        loadingIndicator.SetActive(isLoading);

        foreach (Button button in buttons)
        {
            button.interactable = !isLoading;
        }
    }
}
