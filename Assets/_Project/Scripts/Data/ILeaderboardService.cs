using UnityEngine;

public interface ILeaderboardService
{
    bool IsSignedIn { get; }

    Awaitable<bool> SignInAsync();

    Awaitable<bool> SubmitScoreAsync(LeaderboardEntry entry);
}
