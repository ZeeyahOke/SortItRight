using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private StartScreen startScreen;
    [SerializeField] private SetupScreen setupScreen;
    [SerializeField] private ScenarioSelectScreen scenarioSelectScreen;
    [SerializeField] private HudScreen hudScreen;
    [SerializeField] private PauseScreen pauseScreen;
    [SerializeField] private UIScreen[] allScreens;

    public StartScreen StartScreen => startScreen;
    public SetupScreen SetupScreen => setupScreen;
    public ScenarioSelectScreen ScenarioSelectScreen => scenarioSelectScreen;
    public HudScreen HudScreen => hudScreen;
    public PauseScreen PauseScreen => pauseScreen;

    public void HideAll()
    {
        foreach (UIScreen screen in allScreens)
        {
            screen.Hide();
        }
    }

    public void ShowOnly(UIScreen screen)
    {
        foreach (UIScreen other in allScreens)
        {
            if (other != screen)
            {
                other.Hide();
            }
        }

        screen.Show();
    }
}
