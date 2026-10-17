public readonly struct ItemPickedEvent
{
    public readonly WasteItemData item;

    public ItemPickedEvent(WasteItemData item)
    {
        this.item = item;
    }
}
