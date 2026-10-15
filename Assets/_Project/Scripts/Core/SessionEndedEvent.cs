public readonly struct SessionEndedEvent
{
    public readonly string scenarioId;
    public readonly SessionOutcome outcome;
    public readonly float durationSeconds;

    public SessionEndedEvent(string scenarioId, SessionOutcome outcome, float durationSeconds)
    {
        this.scenarioId = scenarioId;
        this.outcome = outcome;
        this.durationSeconds = durationSeconds;
    }
}
