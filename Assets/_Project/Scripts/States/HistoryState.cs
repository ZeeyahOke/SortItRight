public class HistoryState : IGameState
{
    private const int SessionsShown = 5;

    private readonly GameController game;

    public HistoryState(GameController game)
    {
        this.game = game;
    }

    private HistoryScreen Screen => game.UI.HistoryScreen;

    public void Enter()
    {
        Screen.ShowSessions(DataManager.Instance.RecentSessions(SessionsShown), game.Scenarios);
        game.UI.ShowOnly(Screen);
        Screen.BackButton.onClick.AddListener(OnBackPressed);
    }

    public void Tick()
    {
    }

    public void Exit()
    {
        Screen.BackButton.onClick.RemoveListener(OnBackPressed);
    }

    private void OnBackPressed()
    {
        game.StateMachine.ChangeState(game.StartState);
    }
}
