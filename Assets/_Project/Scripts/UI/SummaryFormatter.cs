using System.Text;
using UnityEngine;

public static class SummaryFormatter
{
    public static string Title(SessionOutcome outcome)
    {
        if (outcome == SessionOutcome.Passed)
        {
            return "Great sorting!";
        }

        return outcome == SessionOutcome.Failed ? "Session failed" : "Session complete";
    }

    public static string Subtitle(SessionOutcome outcome)
    {
        if (outcome == SessionOutcome.Passed)
        {
            return "You passed with no serious errors.";
        }

        if (outcome == SessionOutcome.Failed)
        {
            return "Too many serious errors or rejected bins.";
        }

        int target = Mathf.RoundToInt(SessionStats.PassAccuracy * 100f);
        return "Reach " + target + "% accuracy with no serious errors to pass.";
    }

    public static string Numbers(SessionEndedEvent result)
    {
        SessionStats stats = result.stats;
        int seconds = Mathf.FloorToInt(result.durationSeconds);
        return "Correct: " + stats.Correct
            + "\nWrong bin: " + stats.Incorrect
            + "\nMissed: " + stats.Missed
            + "\nSerious errors: " + stats.SeriousErrors
            + "\nRejected bins: " + stats.RejectedBins
            + "\nAccuracy: " + Mathf.RoundToInt(stats.Accuracy() * 100f) + "%"
            + "\nTime: " + (seconds / 60) + ":" + (seconds % 60).ToString("00");
    }

    public static string Mistakes(SessionStats stats, ScenarioData scenario)
    {
        if (stats.Mistakes.Count == 0)
        {
            return "No mistakes. Well done!";
        }

        StringBuilder builder = new StringBuilder();

        foreach (MistakeRecord mistake in stats.Mistakes)
        {
            string action = mistake.placedIn == SessionStats.MissedLabel ? "missed" : "put in " + mistake.placedIn;
            builder.Append(ItemName(mistake.itemId, scenario)).Append(": ").Append(action);
            builder.Append(", goes in ").Append(mistake.correctBin).Append('\n');
        }

        return builder.ToString().TrimEnd();
    }

    private static string ItemName(string itemId, ScenarioData scenario)
    {
        foreach (WasteItemData item in scenario.ItemPool)
        {
            if (item.ItemId == itemId)
            {
                return item.DisplayName;
            }
        }

        return itemId;
    }
}
