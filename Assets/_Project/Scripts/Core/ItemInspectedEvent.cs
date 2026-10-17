public readonly struct ItemInspectedEvent
{
    public readonly WasteItemData item;

    public ItemInspectedEvent(WasteItemData item)
    {
        this.item = item;
    }
}
