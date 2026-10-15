using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

public class ARPlacementManager : MonoBehaviour
{
    [SerializeField] private PlacementReticle reticle;
    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private Camera arCamera;
    [SerializeField] private Transform station;

    private bool isPlacing = true;

    public bool HasPreview => station.gameObject.activeSelf;
    public bool IsConfirmed => !isPlacing;

    private void Update()
    {
        if (isPlacing && WasTappedThisFrame() && reticle.HasHit)
        {
            PlacePreview(reticle.transform.position);
        }
    }

    public void ConfirmPlacement()
    {
        if (!isPlacing || !HasPreview)
        {
            return;
        }

        isPlacing = false;
        station.gameObject.AddComponent<ARAnchor>();
        reticle.gameObject.SetActive(false);
        SetPlaneDetection(false);
    }

    private void SetPlaneDetection(bool isOn)
    {
        planeManager.enabled = isOn;

        foreach (ARPlane plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(isOn);
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
