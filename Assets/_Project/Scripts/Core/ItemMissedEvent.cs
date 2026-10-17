public readonly struct ItemMissedEvent
{
    public readonly WasteItemData item;
    public readonly SortResult result;
    public readonly bool attractsAnimals;

    public ItemMissedEvent(WasteItemData item, SortResult result, bool attractsAnimals)
    {
        this.item = item;
        this.result = result;
        this.attractsAnimals = attractsAnimals;
    }
}
