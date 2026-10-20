using System;
using System.Collections.Generic;

[Serializable]
public class SessionRecord
{
    public string sessionId;
    public string timestampUtc;
    public string scenarioId;
    public float durationSeconds;
    public int correct;
    public int incorrect;
    public int missed;
    public int seriousErrors;
    public int rejectedBins;
    public float accuracy;
    public int score;
    public string outcome;
    public List<MistakeRecord> mistakes = new List<MistakeRecord>();

    public static SessionRecord From(SessionEndedEvent result)
    {
        SessionStats stats = result.stats;
        SessionRecord record = new SessionRecord
        {
            sessionId = Guid.NewGuid().ToString(),
            timestampUtc = DateTime.UtcNow.ToString("o"),
            scenarioId = result.scenarioId,
            durationSeconds = result.durationSeconds,
            correct = stats.Correct,
            incorrect = stats.Incorrect,
            missed = stats.Missed,
            seriousErrors = stats.SeriousErrors,
            rejectedBins = stats.RejectedBins,
            accuracy = stats.Accuracy(),
            score = stats.Score(),
            outcome = result.outcome.ToString()
        };
        record.mistakes.AddRange(stats.Mistakes);
        return record;
    }
}
