using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummaryScreen : UIScreen
{
    [SerializeField] private Image outcomeBanner;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text subtitleText;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text numbersText;
    [SerializeField] private TMP_Text mistakesText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Color passedColour = new Color(0.18f, 0.62f, 0.36f);
    [SerializeField] private Color completedColour = new Color(0.2f, 0.45f, 0.85f);
    [SerializeField] private Color failedColour = new Color(0.78f, 0.2f, 0.2f);

    public Button RestartButton => restartButton;
    public Button MainMenuButton => mainMenuButton;

    public void ShowResult(SessionEndedEvent result, ScenarioData scenario)
    {
        outcomeBanner.color = ColourFor(result.outcome);
        titleText.text = SummaryFormatter.Title(result.outcome);
        subtitleText.text = SummaryFormatter.Subtitle(result.outcome);
        scoreText.text = "Score: " + result.stats.Score();
        numbersText.text = SummaryFormatter.Numbers(result);
        mistakesText.text = SummaryFormatter.Mistakes(result.stats, scenario);
    }

    private Color ColourFor(SessionOutcome outcome)
    {
        if (outcome == SessionOutcome.Passed)
        {
            return passedColour;
        }

        return outcome == SessionOutcome.Failed ? failedColour : completedColour;
    }
}
