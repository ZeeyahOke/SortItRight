using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public static class HistoryFormatter
{
    public static string Row(SessionRecord record, IReadOnlyList<ScenarioData> scenarios)
    {
        int accuracy = Mathf.RoundToInt(record.accuracy * 100f);
        return "<b>" + ScenarioName(record.scenarioId, scenarios) + "</b>   " + record.outcome
            + "\n" + LocalDate(record.timestampUtc) + "   Score " + record.score + "   " + accuracy + "%";
    }

    private static string LocalDate(string timestampUtc)
    {
        if (DateTime.TryParse(timestampUtc, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out DateTime time))
        {
            return time.ToLocalTime().ToString("d MMM yyyy, HH:mm", CultureInfo.InvariantCulture);
        }

        return string.Empty;
    }

    private static string ScenarioName(string scenarioId, IReadOnlyList<ScenarioData> scenarios)
    {
        foreach (ScenarioData scenario in scenarios)
        {
            if (scenario.ScenarioId == scenarioId)
            {
                return scenario.DisplayName;
            }
        }

        return scenarioId;
    }
}
