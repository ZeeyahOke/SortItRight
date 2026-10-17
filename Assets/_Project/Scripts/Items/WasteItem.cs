using UnityEngine;

public abstract class WasteItem : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject highlight;
    [SerializeField] private ItemModelView modelView;

    public WasteItemData Data { get; private set; }
    public bool IsOnBelt { get; private set; }
    public bool IsSelected { get; private set; }
    public virtual bool AttractsAnimals => false;
    public ItemModelView ModelView => modelView;

    public void Initialise(WasteItemData data)
    {
        Data = data;
        IsOnBelt = true;
        SetSelected(false);
    }

    public abstract SortResult EvaluateSort(BinType bin);

    public abstract SortResult OnMissed();

    public void MarkResolved()
    {
        IsOnBelt = false;
        SetSelected(false);
    }

    public void SetSelected(bool isSelected)
    {
        IsSelected = isSelected;
        highlight.SetActive(isSelected);
    }

    public virtual void ResetForPool()
    {
        Data = null;
        IsOnBelt = false;
        SetSelected(false);
        modelView.Clear();
        transform.localRotation = Quaternion.identity;
    }

    public void OnTap()
    {
        SetSelected(true);
        EventBus.Publish(new ItemPickedEvent(Data));
    }

    public void OnLongPress()
    {
        EventBus.Publish(new ItemInspectedEvent(Data));
    }

    protected SortResult CorrectOrNormalMistake(BinType bin)
    {
        if (bin == Data.CorrectBin)
        {
            return SortResult.Correct(bin);
        }

        return SortResult.Wrong(Data.CorrectBin, ErrorSeverity.Normal);
    }
}
