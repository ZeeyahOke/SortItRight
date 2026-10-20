using System;

[Serializable]
public class LeaderboardEntry
{
    public string nickname;
    public string playerId;
    public string scenarioId;
    public int score;
    public string dateUtc;

    public LeaderboardEntry(string nickname, string scenarioId, int score)
    {
        this.nickname = nickname;
        this.scenarioId = scenarioId;
        this.score = score;
        dateUtc = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
    }
}
