using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardScreen : UIScreen
{
    [SerializeField] private Button tabPrefab;
    [SerializeField] private Transform tabParent;
    [SerializeField] private TMP_Text[] rowTexts;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private Button backButton;
    [SerializeField] private Color selectedTabColour = new Color(0.2f, 0.45f, 0.85f);
    [SerializeField] private Color tabColour = new Color(0.6f, 0.65f, 0.7f);

    private readonly List<Button> tabs = new List<Button>();

    public Button BackButton => backButton;

    public void BuildTabs(IReadOnlyList<ScenarioData> scenarios, Action<int> onTabPicked)
    {
        if (tabs.Count > 0)
        {
            return;
        }

        for (int i = 0; i < scenarios.Count; i++)
        {
            int index = i;
            Button tab = Instantiate(tabPrefab, tabParent);
            tab.GetComponentInChildren<TMP_Text>().text = scenarios[i].DisplayName;
            tab.onClick.AddListener(() => onTabPicked(index));
            tabs.Add(tab);
        }
    }

    public void SelectTab(int selected)
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            tabs[i].image.color = i == selected ? selectedTabColour : tabColour;
        }
    }

    public void ShowStatus(string message)
    {
        statusText.text = message;
        statusText.gameObject.SetActive(true);
        SetRowsVisible(0);
    }

    public void ShowEntries(IReadOnlyList<LeaderboardEntry> entries)
    {
        if (entries.Count == 0)
        {
            ShowStatus("No scores yet. Be the first!");
            return;
        }

        statusText.gameObject.SetActive(false);
        SetRowsVisible(entries.Count);

        for (int i = 0; i < entries.Count && i < rowTexts.Length; i++)
        {
            rowTexts[i].text = LeaderboardFormatter.Row(i + 1, entries[i]);
        }
    }

    private void SetRowsVisible(int count)
    {
        for (int i = 0; i < rowTexts.Length; i++)
        {
            rowTexts[i].transform.parent.gameObject.SetActive(i < count);
        }
    }
}
