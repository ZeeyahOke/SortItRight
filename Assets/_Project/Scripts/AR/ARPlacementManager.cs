using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ARPlacementManager : MonoBehaviour
{
    [SerializeField] private PlacementReticle reticle;
    [SerializeField] private Camera arCamera;
    [SerializeField] private Transform station;

    public bool HasPreview => station.gameObject.activeSelf;

    private void Update()
    {
        if (WasTappedThisFrame() && reticle.HasHit)
        {
            PlacePreview(reticle.transform.position);
        }
    }

    private bool WasTappedThisFrame()
    {
        Pointer pointer = Pointer.current;

        if (pointer == null || !pointer.press.wasPressedThisFrame)
        {
            return false;
        }

        return EventSystem.current == null || !EventSystem.current.IsPointerOverGameObject();
    }

    private void PlacePreview(Vector3 position)
    {
        station.SetPositionAndRotation(position, RotationFacingCamera(position));
        station.gameObject.SetActive(true);
    }

    private Quaternion RotationFacingCamera(Vector3 position)
    {
        Vector3 toCamera = arCamera.transform.position - position;
        toCamera.y = 0f;

        if (toCamera.sqrMagnitude < 0.0001f)
        {
            return Quaternion.identity;
        }

        return Quaternion.LookRotation(toCamera);
    }
}
