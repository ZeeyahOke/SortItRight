public readonly struct SessionEndedEvent
{
    public readonly string scenarioId;
    public readonly SessionOutcome outcome;
    public readonly float durationSeconds;
    public readonly SessionStats stats;

    public SessionEndedEvent(string scenarioId, SessionOutcome outcome, float durationSeconds, SessionStats stats)
    {
        this.scenarioId = scenarioId;
        this.outcome = outcome;
        this.durationSeconds = durationSeconds;
        this.stats = stats;
    }
}
