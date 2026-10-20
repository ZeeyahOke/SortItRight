using System.Text;

public static class FirestoreJson
{
    public static string EntryDocument(LeaderboardEntry entry)
    {
        return "{\"fields\":{"
            + StringField("nickname", entry.nickname) + ","
            + StringField("playerId", entry.playerId) + ","
            + StringField("scenarioId", entry.scenarioId) + ","
            + "\"score\":{\"integerValue\":\"" + entry.score + "\"},"
            + "\"dateUtc\":{\"timestampValue\":\"" + entry.dateUtc + "\"}}}";
    }

    public static string TopScoresQuery(string scenarioId, int count)
    {
        return "{\"structuredQuery\":{\"from\":[{\"collectionId\":\"leaderboard\"}],"
            + "\"where\":{\"fieldFilter\":{\"field\":{\"fieldPath\":\"scenarioId\"},\"op\":\"EQUAL\","
            + "\"value\":{\"stringValue\":\"" + Escape(scenarioId) + "\"}}},"
            + "\"orderBy\":[{\"field\":{\"fieldPath\":\"score\"},\"direction\":\"DESCENDING\"}],"
            + "\"limit\":" + count + "}}";
    }

    private static string StringField(string name, string value)
    {
        return "\"" + name + "\":{\"stringValue\":\"" + Escape(value) + "\"}";
    }

    private static string Escape(string value)
    {
        StringBuilder builder = new StringBuilder();

        foreach (char character in value)
        {
            if (character == '"' || character == '\\')
            {
                builder.Append('\\').Append(character);
            }
            else if (character < ' ')
            {
                builder.Append("\\u").Append(((int)character).ToString("x4"));
            }
            else
            {
                builder.Append(character);
            }
        }

        return builder.ToString();
    }
}
