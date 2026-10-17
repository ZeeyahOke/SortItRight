using UnityEngine.InputSystem;

public class PressTracker
{
    private readonly float longPressSeconds;
    private float pressStartTime;
    private bool isPressing;
    private bool longPressFired;

    public PressTracker(float longPressSeconds)
    {
        this.longPressSeconds = longPressSeconds;
    }

    public PressPhase Read(Pointer pointer, float now)
    {
        if (pointer == null)
        {
            return PressPhase.None;
        }

        if (pointer.press.wasPressedThisFrame)
        {
            isPressing = true;
            longPressFired = false;
            pressStartTime = now;
            return PressPhase.Began;
        }

        if (!isPressing)
        {
            return PressPhase.None;
        }

        if (!pointer.press.isPressed)
        {
            isPressing = false;
            return longPressFired ? PressPhase.LongPressEnded : PressPhase.Tap;
        }

        if (!longPressFired && now - pressStartTime >= longPressSeconds)
        {
            longPressFired = true;
            return PressPhase.LongPress;
        }

        return PressPhase.None;
    }
}
