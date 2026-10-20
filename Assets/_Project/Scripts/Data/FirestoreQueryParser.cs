using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public static class FirestoreQueryParser
{
    public static List<LeaderboardEntry> Entries(string responseJson)
    {
        List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
        FirestoreQueryResults parsed = JsonUtility.FromJson<FirestoreQueryResults>("{\"results\":" + responseJson + "}");

        if (parsed == null || parsed.results == null)
        {
            return entries;
        }

        foreach (FirestoreQueryResult result in parsed.results)
        {
            LeaderboardFields fields = result.document?.fields;

            if (fields?.nickname == null || string.IsNullOrEmpty(fields.nickname.stringValue))
            {
                continue;
            }

            entries.Add(ToEntry(fields));
        }

        return entries;
    }

    private static LeaderboardEntry ToEntry(LeaderboardFields fields)
    {
        int.TryParse(fields.score?.integerValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out int score);
        LeaderboardEntry entry = new LeaderboardEntry(fields.nickname.stringValue, fields.scenarioId?.stringValue, score);
        entry.playerId = fields.playerId?.stringValue;
        entry.dateUtc = fields.dateUtc?.timestampValue;
        return entry;
    }
}
