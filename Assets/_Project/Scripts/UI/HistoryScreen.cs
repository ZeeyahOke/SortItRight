using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HistoryScreen : UIScreen
{
    [SerializeField] private TMP_Text[] rowTexts;
    [SerializeField] private TMP_Text emptyText;
    [SerializeField] private Button backButton;

    public Button BackButton => backButton;

    public void ShowSessions(IReadOnlyList<SessionRecord> sessions, IReadOnlyList<ScenarioData> scenarios)
    {
        emptyText.gameObject.SetActive(sessions.Count == 0);

        for (int i = 0; i < rowTexts.Length; i++)
        {
            bool hasSession = i < sessions.Count;
            rowTexts[i].transform.parent.gameObject.SetActive(hasSession);

            if (hasSession)
            {
                rowTexts[i].text = HistoryFormatter.Row(sessions[i], scenarios);
            }
        }
    }
}
