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

    private int shownItemsLeft = -1;
    private int shownAccuracy = -1;
    private int shownSerious = -1;
    private int shownSeconds = -1;

    public Button PauseButton => pauseButton;

    public void Refresh(SimulationManager simulation)
    {
        SessionStats stats = simulation.Stats;

        if (HasChanged(ref shownItemsLeft, simulation.ItemsLeft))
        {
            itemsLeftText.text = shownItemsLeft.ToString();
        }

        if (HasChanged(ref shownAccuracy, Mathf.RoundToInt(stats.Accuracy() * 100f)))
        {
            accuracyText.text = shownAccuracy + "%";
        }

        if (HasChanged(ref shownSerious, stats.SeriousErrors))
        {
            seriousText.text = shownSerious + "/" + SessionRules.SeriousErrorsToFail;
        }

        if (HasChanged(ref shownSeconds, Mathf.FloorToInt(simulation.ElapsedSeconds)))
        {
            timeText.text = (shownSeconds / 60) + ":" + (shownSeconds % 60).ToString("00");
        }

        RefreshBins(simulation);
    }

    private void RefreshBins(SimulationManager simulation)
    {
        int count = Mathf.Min(binChips.Length, simulation.Bins.Count);

        for (int i = 0; i < count; i++)
        {
            Bin bin = simulation.Bins[i];
            binChips[i].SetStrikes(bin.Strikes, bin.StrikesToReject);
        }
    }

    private static bool HasChanged(ref int shown, int current)
    {
        if (shown == current)
        {
            return false;
        }

        shown = current;
        return true;
    }
}
