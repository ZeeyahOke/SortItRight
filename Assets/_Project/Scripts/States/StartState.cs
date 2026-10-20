public class StartState : IGameState
{
    private readonly GameController game;

    public StartState(GameController game)
    {
        this.game = game;
    }

    public void Enter()
    {
        game.Placement.enabled = false;
        game.Reticle.gameObject.SetActive(false);
        game.UI.ShowOnly(game.UI.StartScreen);
        game.UI.StartScreen.StartButton.onClick.AddListener(OnStartPressed);
        game.UI.StartScreen.HistoryButton.onClick.AddListener(OnHistoryPressed);
    }

    public void Tick()
    {
    }

    public void Exit()
    {
        game.UI.StartScreen.StartButton.onClick.RemoveListener(OnStartPressed);
        game.UI.StartScreen.HistoryButton.onClick.RemoveListener(OnHistoryPressed);
    }

    private void OnStartPressed()
    {
        game.StateMachine.ChangeState(game.SetupState);
    }

    private void OnHistoryPressed()
    {
        game.StateMachine.ChangeState(game.HistoryState);
    }
}
