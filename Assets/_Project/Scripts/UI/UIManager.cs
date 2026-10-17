using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private StartScreen startScreen;
    [SerializeField] private SetupScreen setupScreen;
    [SerializeField] private ScenarioSelectScreen scenarioSelectScreen;
    [SerializeField] private UIScreen[] allScreens;

    public StartScreen StartScreen => startScreen;
    public SetupScreen SetupScreen => setupScreen;
    public ScenarioSelectScreen ScenarioSelectScreen => scenarioSelectScreen;

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
