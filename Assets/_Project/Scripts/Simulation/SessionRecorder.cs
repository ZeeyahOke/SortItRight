public class SessionRecorder
{
    private readonly SessionStats stats;

    public SessionRecorder(SessionStats stats)
    {
        this.stats = stats;
    }

    public void StartListening()
    {
        EventBus.Subscribe<ItemSortedEvent>(OnItemSorted);
        EventBus.Subscribe<ItemMissedEvent>(OnItemMissed);
        EventBus.Subscribe<BinRejectedEvent>(OnBinRejected);
    }

    public void StopListening()
    {
        EventBus.Unsubscribe<ItemSortedEvent>(OnItemSorted);
        EventBus.Unsubscribe<ItemMissedEvent>(OnItemMissed);
        EventBus.Unsubscribe<BinRejectedEvent>(OnBinRejected);
    }

    private void OnItemSorted(ItemSortedEvent e)
    {
        stats.RecordSort(e.item.ItemId, e.placedIn, e.result);
        ReportSeriousError(e.item, e.result, false);
    }

    private void OnItemMissed(ItemMissedEvent e)
    {
        stats.RecordMiss(e.item.ItemId, e.result);
        ReportSeriousError(e.item, e.result, true);
    }

    private void OnBinRejected(BinRejectedEvent e)
    {
        stats.RecordRejectedBin();
    }

    private void ReportSeriousError(WasteItemData item, SortResult result, bool wasMissed)
    {
        if (result.severity == ErrorSeverity.Serious)
        {
            EventBus.Publish(new SeriousErrorEvent(item, wasMissed));
        }
    }
}
