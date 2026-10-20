using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SimulationState : IGameState
{
    private readonly GameController game;

    public SimulationState(GameController game)
    {
        this.game = game;
    }

    public void Enter()
    {
        Time.timeScale = 1f;
        game.UI.ShowOnly(game.UI.HudScreen);
        game.Interaction.enabled = true;
        game.UI.HudScreen.PauseButton.onClick.AddListener(OnPausePressed);
        ARSession.stateChanged += OnSessionStateChanged;
        EventBus.Subscribe<SessionEndedEvent>(OnSessionEnded);

        if (!game.Simulation.IsRunning)
        {
            game.Simulation.StartSession(game.SelectedScenario, game.Loader.Models);
        }
    }

    public void Tick()
    {
        game.UI.HudScreen.Refresh(game.Simulation);
    }

    public void Exit()
    {
        game.Interaction.enabled = false;
        game.UI.HudScreen.PauseButton.onClick.RemoveListener(OnPausePressed);
        ARSession.stateChanged -= OnSessionStateChanged;
        EventBus.Unsubscribe<SessionEndedEvent>(OnSessionEnded);
    }

    private void OnPausePressed()
    {
        Pause(false);
    }

    private void OnSessionStateChanged(ARSessionStateChangedEventArgs args)
    {
        if (args.state == ARSessionState.SessionInitializing)
        {
            Pause(true);
        }
    }

    private void OnSessionEnded(SessionEndedEvent e)
    {
        game.SummaryState.Result = e;
        game.StateMachine.ChangeState(game.SummaryState);
    }

    private void Pause(bool trackingLost)
    {
        game.PausedState.TrackingLost = trackingLost;
        game.StateMachine.ChangeState(game.PausedState);
    }
}
