using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudScreen : UIScreen
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private TMP_Text itemsLeftText;
    [SerializeField] private TMP_Text accuracyText;
    [SerializeField] private TMP_Text seriousText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private BinStrikeChip[] binChips;

    private bool statsChanged = true;
    private int shownSeconds = -1;

    public Button PauseButton => pauseButton;

    private void OnEnable()
    {
        statsChanged = true;
        shownSeconds = -1;
        EventBus.Subscribe<ItemSortedEvent>(OnItemSorted);
        EventBus.Subscribe<ItemMissedEvent>(OnItemMissed);
        EventBus.Subscribe<BinRejectedEvent>(OnBinRejected);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<ItemSortedEvent>(OnItemSorted);
        EventBus.Unsubscribe<ItemMissedEvent>(OnItemMissed);
        EventBus.Unsubscribe<BinRejectedEvent>(OnBinRejected);
    }

    public void Refresh(SimulationManager simulation)
    {
        int seconds = Mathf.FloorToInt(simulation.ElapsedSeconds);

        if (seconds != shownSeconds)
        {
            shownSeconds = seconds;
            timeText.SetText("{0}:{1:00}", (float)(seconds / 60), seconds % 60);
        }

        if (statsChanged)
        {
            statsChanged = false;
            ShowStats(simulation);
        }
    }

    private void ShowStats(SimulationManager simulation)
    {
        SessionStats stats = simulation.Stats;
        itemsLeftText.SetText("{0}", (float)simulation.ItemsLeft);
        accuracyText.SetText("{0}%", Mathf.Round(stats.Accuracy() * 100f));
        seriousText.SetText("{0}/{1}", (float)stats.SeriousErrors, SessionRules.SeriousErrorsToFail);
        int count = Mathf.Min(binChips.Length, simulation.Bins.Count);

        for (int i = 0; i < count; i++)
        {
            Bin bin = simulation.Bins[i];
            binChips[i].SetStrikes(bin.Strikes, bin.StrikesToReject);
        }
    }

    private void OnItemSorted(ItemSortedEvent e)
    {
        statsChanged = true;
    }

    private void OnItemMissed(ItemMissedEvent e)
    {
        statsChanged = true;
    }

    private void OnBinRejected(BinRejectedEvent e)
    {
        statsChanged = true;
    }
}
