using UnityEngine;
using UnityEngine.UI;

public class HudScreen : UIScreen
{
    [SerializeField] private Button pauseButton;

    public Button PauseButton => pauseButton;
}
