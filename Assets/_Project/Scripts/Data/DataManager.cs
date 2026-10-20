using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private ISessionRepository sessions;
    private ILeaderboardService leaderboard;

    public static DataManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        sessions = new JsonSessionRepository(Application.persistentDataPath);
        FirebaseConfig config = FirebaseConfig.Load();

        if (config != null && config.IsValid)
        {
            leaderboard = new FirebaseLeaderboardService(config);
        }
    }

    private async void Start()
    {
        if (leaderboard != null)
        {
            await leaderboard.SignInAsync();
        }
    }

    private void OnEnable()
    {
        EventBus.Subscribe<SessionEndedEvent>(OnSessionEnded);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<SessionEndedEvent>(OnSessionEnded);
    }

    public IReadOnlyList<SessionRecord> RecentSessions(int count)
    {
        return sessions.LoadRecent(count);
    }

    public async Awaitable<bool> SubmitScoreAsync(string nickname, string scenarioId, int score)
    {
        if (leaderboard == null)
        {
            return false;
        }

        return await leaderboard.SubmitScoreAsync(new LeaderboardEntry(nickname, scenarioId, score));
    }

    public async Awaitable<List<LeaderboardEntry>> GetTopScoresAsync(string scenarioId, int count)
    {
        if (leaderboard == null)
        {
            return null;
        }

        return await leaderboard.GetTopScoresAsync(scenarioId, count);
    }

    private void OnSessionEnded(SessionEndedEvent e)
    {
        sessions.Save(SessionRecord.From(e));
    }
}
