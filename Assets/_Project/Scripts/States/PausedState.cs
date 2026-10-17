using UnityEngine;

public class PausedState : IGameState
{
    private const string PausedMessage = "The belt is stopped. Take your time.";
    private const string TrackingLostMessage = "Tracking lost. Point your phone at the station, then tap Resume.";

    private readonly GameController game;

    public PausedState(GameController game)
    {
        this.game = game;
    }

    public bool TrackingLost { get; set; }

    private PauseScreen Screen => game.UI.PauseScreen;

    public void Enter()
    {
        Time.timeScale = 0f;
        Screen.SetMessage(TrackingLost ? TrackingLostMessage : PausedMessage);
        game.UI.ShowOnly(Screen);
        Screen.ResumeButton.onClick.AddListener(OnResumePressed);
        Screen.ResetButton.onClick.AddListener(OnResetPressed);
        Screen.MainMenuButton.onClick.AddListener(OnMainMenuPressed);
    }

    public void Tick()
    {
    }

    public void Exit()
    {
        TrackingLost = false;
        Screen.ResumeButton.onClick.RemoveListener(OnResumePressed);
        Screen.ResetButton.onClick.RemoveListener(OnResetPressed);
        Screen.MainMenuButton.onClick.RemoveListener(OnMainMenuPressed);
    }

    private void OnResumePressed()
    {
        game.StateMachine.ChangeState(game.SimulationState);
    }

    private void OnResetPressed()
    {
        game.StateMachine.ChangeState(game.SetupState);
    }

    private void OnMainMenuPressed()
    {
        game.ReturnToMainMenu();
    }
}
