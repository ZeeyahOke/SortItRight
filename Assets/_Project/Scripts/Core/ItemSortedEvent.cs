public readonly struct ItemSortedEvent
{
    public readonly WasteItemData item;
    public readonly BinType placedIn;
    public readonly SortResult result;

    public ItemSortedEvent(WasteItemData item, BinType placedIn, SortResult result)
    {
        this.item = item;
        this.placedIn = placedIn;
        this.result = result;
    }
}
