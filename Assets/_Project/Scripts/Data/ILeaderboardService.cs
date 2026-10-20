using UnityEngine;

public interface ILeaderboardService
{
    bool IsSignedIn { get; }

    Awaitable<bool> SignInAsync();
}
