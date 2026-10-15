public readonly struct ItemMissedEvent
{
    public readonly WasteItemData item;
    public readonly SortResult result;

    public ItemMissedEvent(WasteItemData item, SortResult result)
    {
        this.item = item;
        this.result = result;
    }
}
