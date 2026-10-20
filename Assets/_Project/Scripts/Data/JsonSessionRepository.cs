using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonSessionRepository : ISessionRepository
{
    private const int MaxSessions = 50;

    private readonly string filePath;
    private readonly string tempPath;
    private SessionHistory history;

    public JsonSessionRepository(string folder)
    {
        filePath = Path.Combine(folder, "sessions.json");
        tempPath = filePath + ".tmp";
    }

    public void Save(SessionRecord record)
    {
        SessionHistory current = Load();
        current.sessions.Add(record);

        if (current.sessions.Count > MaxSessions)
        {
            current.sessions.RemoveRange(0, current.sessions.Count - MaxSessions);
        }

        WriteSafely(JsonUtility.ToJson(current));
    }

    public IReadOnlyList<SessionRecord> LoadRecent(int count)
    {
        List<SessionRecord> sessions = Load().sessions;
        List<SessionRecord> recent = new List<SessionRecord>();

        for (int i = sessions.Count - 1; i >= 0 && recent.Count < count; i--)
        {
            recent.Add(sessions[i]);
        }

        return recent;
    }

    private SessionHistory Load()
    {
        if (history != null)
        {
            return history;
        }

        history = ReadFile() ?? new SessionHistory();
        return history;
    }

    private SessionHistory ReadFile()
    {
        try
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            SessionHistory loaded = JsonUtility.FromJson<SessionHistory>(File.ReadAllText(filePath));
            return loaded != null && loaded.sessions != null ? loaded : null;
        }
        catch (Exception exception)
        {
            Debug.LogWarning("Session history could not be read: " + exception.Message);
            return null;
        }
    }

    private void WriteSafely(string json)
    {
        try
        {
            File.WriteAllText(tempPath, json);

            if (File.Exists(filePath))
            {
                File.Replace(tempPath, filePath, null);
            }
            else
            {
                File.Move(tempPath, filePath);
            }
        }
        catch (Exception exception)
        {
            Debug.LogWarning("Session history could not be saved: " + exception.Message);
        }
    }
}
