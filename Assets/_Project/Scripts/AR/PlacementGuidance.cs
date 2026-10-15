using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlacementGuidance : MonoBehaviour
{
    private const string MoveSlowlyMessage = "Move your phone slowly";
    private const string TapToPlaceMessage = "Tap a surface to place the station";
    private const string ConfirmMessage = "Tap again to move it, or press Confirm";

    [SerializeField] private ARPlaneManager planeManager;
    [SerializeField] private ARPlacementManager placementManager;
    [SerializeField] private TMP_Text label;

    private string currentMessage;

    private void Update()
    {
        ShowMessage(ChooseMessage());
    }

    private string ChooseMessage()
    {
        if (placementManager.HasPreview)
        {
            return ConfirmMessage;
        }

        if (planeManager.trackables.count == 0)
        {
            return MoveSlowlyMessage;
        }

        return TapToPlaceMessage;
    }

    private void ShowMessage(string message)
    {
        if (message == currentMessage)
        {
            return;
        }

        currentMessage = message;
        label.text = message;
    }
}
