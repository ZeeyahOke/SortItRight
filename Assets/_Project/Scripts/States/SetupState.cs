public class SetupState : IGameState
{
    private readonly GameController game;

    public SetupState(GameController game)
    {
        this.game = game;
    }

    public void Enter()
    {
        game.Placement.enabled = true;
        game.Placement.ResetPlacement();
        game.UI.ShowOnly(game.UI.SetupScreen);
        game.UI.SetupScreen.SetGuidanceActive(true);
        game.UI.SetupScreen.SetConfirmVisible(false);
        game.UI.SetupScreen.ConfirmButton.onClick.AddListener(OnConfirmPressed);
    }

    public void Tick()
    {
        bool canConfirm = game.Placement.HasPreview && !game.Placement.IsConfirmed;
        game.UI.SetupScreen.SetConfirmVisible(canConfirm);
    }

    public void Exit()
    {
        game.UI.SetupScreen.ConfirmButton.onClick.RemoveListener(OnConfirmPressed);
    }

    private void OnConfirmPressed()
    {
        game.Placement.ConfirmPlacement();
        game.UI.SetupScreen.SetGuidanceActive(false);

        bool canRestart = game.SelectedScenario != null && game.Loader.IsLoaded;

        if (game.Simulation.IsRunning || canRestart)
        {
            game.StateMachine.ChangeState(game.SimulationState);
            return;
        }

        game.StateMachine.ChangeState(game.ScenarioSelectState);
    }
}
