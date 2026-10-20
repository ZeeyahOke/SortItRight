public readonly struct SessionStartedEvent
{
    public readonly string scenarioId;

    public SessionStartedEvent(string scenarioId)
    {
        this.scenarioId = scenarioId;
    }
}
