using UnityEngine;

public class FirebaseLeaderboardService : ILeaderboardService
{
    private const string FirestoreUrl = "https://firestore.googleapis.com/v1/projects/";
    private const string LeaderboardPath = "/databases/(default)/documents/leaderboard";

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

    public async Awaitable<bool> SubmitScoreAsync(LeaderboardEntry entry)
    {
        if (!await auth.SignInAsync())
        {
            return false;
        }

        entry.playerId = auth.UserId;
        string url = FirestoreUrl + projectId + LeaderboardPath;
        string body = FirestoreJson.EntryDocument(entry);
        string response = await FirebaseHttp.SendAsync("POST", url, body, "application/json", auth.IdToken);
        return response != null;
    }
}
