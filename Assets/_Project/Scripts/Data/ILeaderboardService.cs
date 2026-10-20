using System.Collections.Generic;
using UnityEngine;

public interface ILeaderboardService
{
    bool IsSignedIn { get; }

    Awaitable<bool> SignInAsync();

    Awaitable<bool> SubmitScoreAsync(LeaderboardEntry entry);

    Awaitable<List<LeaderboardEntry>> GetTopScoresAsync(string scenarioId, int count);
}
