using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    [SerializeField] private float maxDistance = 5f;

    private WasteItem heldItem;

    private void OnDisable()
    {
        ClearHeldItem();
    }

    private void Update()
    {
        Pointer pointer = Pointer.current;

        if (pointer == null || !pointer.press.wasPressedThisFrame || IsOverUI())
        {
            return;
        }

        IInteractable target = FindTarget(pointer.position.ReadValue());

        if (target != null)
        {
            HandleTap(target);
        }
    }

    private void HandleTap(IInteractable target)
    {
        if (target is WasteItem item)
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

    private IInteractable FindTarget(Vector2 screenPoint)
    {
        Ray ray = arCamera.ScreenPointToRay(screenPoint);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            return hit.collider.GetComponentInParent<IInteractable>();
        }

        return null;
    }

    private bool IsOverUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}
