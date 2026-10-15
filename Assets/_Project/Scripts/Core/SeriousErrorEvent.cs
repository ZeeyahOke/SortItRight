public readonly struct SeriousErrorEvent
{
    public readonly WasteItemData item;
    public readonly bool wasMissed;

    public SeriousErrorEvent(WasteItemData item, bool wasMissed)
    {
        this.item = item;
        this.wasMissed = wasMissed;
    }
}
