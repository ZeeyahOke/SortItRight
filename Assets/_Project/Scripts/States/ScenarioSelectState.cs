public class ScenarioSelectState : IGameState
{
    private readonly GameController game;
    private bool isActive;

    public ScenarioSelectState(GameController game)
    {
        this.game = game;
    }

    private ScenarioSelectScreen Screen => game.UI.ScenarioSelectScreen;

    public void Enter()
    {
        isActive = true;
        Screen.Build(game.Scenarios, OnScenarioPicked);
        Screen.SetLoading(false);
        game.UI.ShowOnly(Screen);
    }

    public void Tick()
    {
    }

    public void Exit()
    {
        isActive = false;
    }

    private async void OnScenarioPicked(ScenarioData scenario)
    {
        if (!isActive)
        {
            return;
        }

        Screen.SetLoading(true);
        bool loaded = await game.Loader.LoadAsync(scenario.ScenarioId);

        if (!isActive)
        {
            return;
        }

        Screen.SetLoading(false);

        if (loaded)
        {
            game.SelectedScenario = scenario;
        }
    }
}
