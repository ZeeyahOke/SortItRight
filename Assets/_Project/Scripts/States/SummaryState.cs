public class SummaryState : IGameState
{
    private readonly GameController game;

    public SummaryState(GameController game)
    {
        this.game = game;
    }

    public SessionEndedEvent Result { get; set; }

    private SummaryScreen Screen => game.UI.SummaryScreen;

    public void Enter()
    {
        Screen.ShowResult(Result, game.SelectedScenario);
        game.UI.ShowOnly(Screen);
        Screen.RestartButton.onClick.AddListener(OnRestartPressed);
        Screen.MainMenuButton.onClick.AddListener(OnMainMenuPressed);
    }

    public void Tick()
    {
    }

    public void Exit()
    {
        Screen.RestartButton.onClick.RemoveListener(OnRestartPressed);
        Screen.MainMenuButton.onClick.RemoveListener(OnMainMenuPressed);
    }

    private void OnRestartPressed()
    {
        game.StateMachine.ChangeState(game.SetupState);
    }

    private void OnMainMenuPressed()
    {
        game.ReturnToMainMenu();
    }
}
