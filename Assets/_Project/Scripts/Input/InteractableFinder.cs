using UnityEngine;
using UnityEngine.EventSystems;

public class InteractableFinder
{
    private readonly Camera camera;
    private readonly float maxDistance;

    public InteractableFinder(Camera camera, float maxDistance)
    {
        this.camera = camera;
        this.maxDistance = maxDistance;
    }

    public IInteractable Find(Vector2 screenPoint)
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            return null;
        }

        Ray ray = camera.ScreenPointToRay(screenPoint);

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            return hit.collider.GetComponentInParent<IInteractable>();
        }

        return null;
    }
}
