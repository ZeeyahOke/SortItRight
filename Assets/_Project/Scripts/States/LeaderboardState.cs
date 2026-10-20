using System.Collections.Generic;

public class LeaderboardState : IGameState
{
    private const int EntriesShown = 10;
    private const string OfflineMessage = "Connect to the internet to see the leaderboard";

    private readonly GameController game;
    private bool isActive;
    private int requestId;

    public LeaderboardState(GameController game)
    {
        this.game = game;
    }

    private LeaderboardScreen Screen => game.UI.LeaderboardScreen;

    public void Enter()
    {
        isActive = true;
        Screen.BuildTabs(game.Scenarios, OnTabPicked);
        game.UI.ShowOnly(Screen);
        Screen.BackButton.onClick.AddListener(OnBackPressed);
        OnTabPicked(0);
    }

    public void Tick()
    {
    }

    public void Exit()
    {
        isActive = false;
        Screen.BackButton.onClick.RemoveListener(OnBackPressed);
    }

    private async void OnTabPicked(int index)
    {
        int request = ++requestId;
        Screen.SelectTab(index);
        Screen.ShowStatus("Loading...");
        string scenarioId = game.Scenarios[index].ScenarioId;
        List<LeaderboardEntry> entries = await DataManager.Instance.GetTopScoresAsync(scenarioId, EntriesShown);

        if (!isActive || request != requestId)
        {
            return;
        }

        if (entries == null)
        {
            Screen.ShowStatus(OfflineMessage);
            return;
        }

        Screen.ShowEntries(entries);
    }

    private void OnBackPressed()
    {
        game.StateMachine.ChangeState(game.StartState);
    }
}
