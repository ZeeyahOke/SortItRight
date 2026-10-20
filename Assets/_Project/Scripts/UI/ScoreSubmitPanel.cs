using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSubmitPanel : MonoBehaviour
{
    private const string FailedMessage = "Your score could not be submitted";

    [SerializeField] private TMP_InputField nicknameInput;
    [SerializeField] private Button submitButton;
    [SerializeField] private TMP_Text statusText;

    private string scenarioId;
    private int score;

    private void OnEnable()
    {
        submitButton.onClick.AddListener(OnSubmitPressed);
    }

    private void OnDisable()
    {
        submitButton.onClick.RemoveListener(OnSubmitPressed);
    }

    public void Prepare(string sessionScenarioId, int sessionScore)
    {
        scenarioId = sessionScenarioId;
        score = sessionScore;
        statusText.text = string.Empty;
        SetInteractable(true);
    }

    private async void OnSubmitPressed()
    {
        string nickname = nicknameInput.text.Trim();

        if (nickname.Length == 0)
        {
            statusText.text = "Enter a nickname first.";
            return;
        }

        SetInteractable(false);
        statusText.text = "Submitting...";
        bool submitted = await DataManager.Instance.SubmitScoreAsync(nickname, scenarioId, score);

        if (this == null)
        {
            return;
        }

        statusText.text = submitted ? "Score submitted!" : FailedMessage;
        SetInteractable(!submitted);
    }

    private void SetInteractable(bool isInteractable)
    {
        nicknameInput.interactable = isInteractable;
        submitButton.interactable = isInteractable;
    }
}
