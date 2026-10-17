public class SimulationState : IGameState
{
    private readonly GameController game;

    public SimulationState(GameController game)
    {
        this.game = game;
    }

    public void Enter()
    {
        game.UI.HideAll();

        if (!game.Simulation.IsRunning)
        {
            game.Simulation.StartSession(game.SelectedScenario, game.Loader.Models);
        }
    }

    public void Tick()
    {
    }

    public void Exit()
    {
    }
}
