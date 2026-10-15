using UnityEngine;
using UnityEngine.UI;

public class SetupScreen : UIScreen
{
    [SerializeField] private Button confirmButton;
    [SerializeField] private PlacementGuidance guidance;

    public Button ConfirmButton => confirmButton;

    public void SetConfirmVisible(bool isVisible)
    {
        if (confirmButton.gameObject.activeSelf != isVisible)
        {
            confirmButton.gameObject.SetActive(isVisible);
        }
    }

    public void SetGuidanceActive(bool isActive)
    {
        guidance.enabled = isActive;
    }
}
