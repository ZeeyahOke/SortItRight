using UnityEngine;
using UnityEngine.UI;

public class StartScreen : UIScreen
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button historyButton;
    [SerializeField] private Button leaderboardButton;

    public Button StartButton => startButton;
    public Button HistoryButton => historyButton;
    public Button LeaderboardButton => leaderboardButton;
}
