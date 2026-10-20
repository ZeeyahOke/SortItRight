using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private ISessionRepository sessions;

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

    private void OnSessionEnded(SessionEndedEvent e)
    {
        sessions.Save(SessionRecord.From(e));
    }
}
