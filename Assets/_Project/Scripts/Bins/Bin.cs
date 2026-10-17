using UnityEngine;

public class Bin : MonoBehaviour, IInteractable
{
    [SerializeField] private BinType type;
    [SerializeField] private int capacity = 8;

    public BinType Type => type;
    public int Capacity => capacity;
    public int FillLevel { get; private set; }
    public int Strikes { get; private set; }
    public BinState State { get; private set; }

    public SortResult TryAccept(WasteItem item)
    {
        WasteItemData data = item.Data;
        SortResult result = item.EvaluateSort(type);
        item.MarkResolved();

        if (result.correct)
        {
            FillLevel++;
        }
        else
        {
            Strikes++;
        }

        EventBus.Publish(new ItemSortedEvent(data, type, result));
        return result;
    }

    public void Empty()
    {
        FillLevel = 0;
        State = BinState.Accepting;
    }

    public void ResetBin()
    {
        FillLevel = 0;
        Strikes = 0;
        State = BinState.Accepting;
    }

    public void OnTap()
    {
    }

    public void OnLongPress()
    {
    }
}
