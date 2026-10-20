using System.Collections.Generic;
using UnityEngine;

public class FirebaseLeaderboardService : ILeaderboardService
{
    private const string FirestoreUrl = "https://firestore.googleapis.com/v1/projects/";
    private const string DocumentsPath = "/databases/(default)/documents";

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
        string url = FirestoreUrl + projectId + DocumentsPath + "/leaderboard";
        string body = FirestoreJson.EntryDocument(entry);
        string response = await FirebaseHttp.SendAsync("POST", url, body, "application/json", auth.IdToken);
        return response != null;
    }

    public async Awaitable<List<LeaderboardEntry>> GetTopScoresAsync(string scenarioId, int count)
    {
        string url = FirestoreUrl + projectId + DocumentsPath + ":runQuery";
        string body = FirestoreJson.TopScoresQuery(scenarioId, count);
        string response = await FirebaseHttp.SendAsync("POST", url, body, "application/json", null);
        return response == null ? null : FirestoreQueryParser.Entries(response);
    }
}
