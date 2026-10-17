using UnityEngine;

public class Bin : MonoBehaviour, IInteractable
{
    [SerializeField] private BinType type;
    [SerializeField] private int capacity = 8;
    [SerializeField] private int strikesToReject = 3;

    public BinType Type => type;
    public int Capacity => capacity;
    public int StrikesToReject => strikesToReject;
    public int FillLevel { get; private set; }
    public int Strikes { get; private set; }
    public BinState State { get; private set; }
    public bool IsFull => State == BinState.Full;

    public SortResult TryAccept(WasteItem item)
    {
        if (IsFull)
        {
            EventBus.Publish(new BinFullEvent(type));
            return SortResult.Refused(type);
        }

        if (State == BinState.Rejected)
        {
            State = BinState.Accepting;
        }

        WasteItemData data = item.Data;
        SortResult result = item.EvaluateSort(type);
        item.MarkResolved();

        if (result.correct)
        {
            AddToFill();
        }
        else
        {
            AddStrike();
        }

        EventBus.Publish(new ItemSortedEvent(data, type, result));
        return result;
    }

    public void Empty()
    {
        FillLevel = 0;
        State = BinState.Accepting;
        EventBus.Publish(new BinEmptiedEvent(type));
    }

    public void ResetBin()
    {
        FillLevel = 0;
        Strikes = 0;
        State = BinState.Accepting;
    }

    public void OnTap()
    {
        if (IsFull)
        {
            Empty();
        }
    }

    private void AddStrike()
    {
        Strikes++;

        if (Strikes >= strikesToReject)
        {
            FillLevel = 0;
            Strikes = 0;
            State = BinState.Rejected;
            EventBus.Publish(new BinRejectedEvent(type));
        }
    }

    private void AddToFill()
    {
        FillLevel++;

        if (FillLevel >= capacity)
        {
            State = BinState.Full;
            EventBus.Publish(new BinFullEvent(type));
        }
    }

    public void OnLongPress()
    {
    }
}
