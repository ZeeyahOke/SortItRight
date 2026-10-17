using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : UIScreen
{
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button mainMenuButton;

    public Button ResumeButton => resumeButton;
    public Button ResetButton => resetButton;
    public Button MainMenuButton => mainMenuButton;

    public void SetMessage(string message)
    {
        messageText.text = message;
    }
}
