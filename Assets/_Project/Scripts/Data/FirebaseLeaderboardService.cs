using UnityEngine;

public class FirebaseLeaderboardService : ILeaderboardService
{
    private readonly FirebaseAuthClient auth;
    private readonly string projectId;

    public FirebaseLeaderboardService(FirebaseConfig config)
    {
        auth = new FirebaseAuthClient(config.apiKey);
        projectId = config.projectId;
    }

    public bool IsSignedIn => auth.IsSignedIn;

    public Awaitable<bool> SignInAsync()
    {
        return auth.SignInAsync();
    }
}
