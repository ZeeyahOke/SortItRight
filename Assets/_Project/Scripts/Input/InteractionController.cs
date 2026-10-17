using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    [SerializeField] private float maxDistance = 5f;
    [SerializeField] private float longPressSeconds = 0.5f;

    private PressTracker pressTracker;
    private InteractableFinder finder;
    private IInteractable pressTarget;
    private WasteItem heldItem;

    private void Awake()
    {
        pressTracker = new PressTracker(longPressSeconds);
        finder = new InteractableFinder(arCamera, maxDistance);
    }

    private void OnDisable()
    {
        ClearHeldItem();
    }

    private void Update()
    {
        Pointer pointer = Pointer.current;
        PressPhase phase = pressTracker.Read(pointer, Time.unscaledTime);

        if (phase == PressPhase.Began)
        {
            pressTarget = finder.Find(pointer.position.ReadValue());
        }
        else if (phase == PressPhase.LongPress)
        {
            pressTarget?.OnLongPress();
        }
        else if (phase == PressPhase.LongPressEnded)
        {
            EventBus.Publish(new InspectEndedEvent());
        }
        else if (phase == PressPhase.Tap && pressTarget != null)
        {
            HandleTap(pressTarget);
        }
    }

    private void HandleTap(IInteractable target)
    {
        if (target is WasteItem item && item.IsOnBelt)
        {
            SelectItem(item);
        }
        else if (target is Bin bin)
        {
            HandleBinTap(bin);
        }
    }

    private void SelectItem(WasteItem item)
    {
        ClearHeldItem();
        heldItem = item;
        item.OnTap();
    }

    private void HandleBinTap(Bin bin)
    {
        if (bin.IsFull || !HasHeldItem())
        {
            bin.OnTap();
            return;
        }

        bin.TryAccept(heldItem);
        heldItem = null;
    }

    private bool HasHeldItem()
    {
        return heldItem != null && heldItem.IsOnBelt && heldItem.IsSelected;
    }

    private void ClearHeldItem()
    {
        if (HasHeldItem())
        {
            heldItem.SetSelected(false);
        }

        heldItem = null;
    }
}
