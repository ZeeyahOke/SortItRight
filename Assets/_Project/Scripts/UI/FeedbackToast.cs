using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackToast : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Image background;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text bodyText;
    [SerializeField] private TMP_Text warningText;
    [SerializeField] private Color correctColour = new Color(0.18f, 0.62f, 0.36f);
    [SerializeField] private Color mistakeColour = new Color(0.85f, 0.5f, 0.1f);
    [SerializeField] private Color seriousColour = new Color(0.78f, 0.2f, 0.2f);
    [SerializeField] private float showSeconds = 3.5f;

    private float hideTimer;

    private void OnEnable()
    {
        panel.SetActive(false);
        EventBus.Subscribe<ItemSortedEvent>(OnItemSorted);
        EventBus.Subscribe<ItemMissedEvent>(OnItemMissed);
        EventBus.Subscribe<BinFullEvent>(OnBinFull);
        EventBus.Subscribe<BinRejectedEvent>(OnBinRejected);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe<ItemSortedEvent>(OnItemSorted);
        EventBus.Unsubscribe<ItemMissedEvent>(OnItemMissed);
        EventBus.Unsubscribe<BinFullEvent>(OnBinFull);
        EventBus.Unsubscribe<BinRejectedEvent>(OnBinRejected);
    }

    private void Update()
    {
        if (!panel.activeSelf)
        {
            return;
        }

        hideTimer -= Time.deltaTime;

        if (hideTimer <= 0f)
        {
            panel.SetActive(false);
        }
    }

    private void OnItemSorted(ItemSortedEvent e)
    {
        string warning = FeedbackMessages.Warning(e.item, e.result, false);
        Show(FeedbackMessages.SortTitle(e.result), FeedbackMessages.Body(e.item), warning, ColourFor(e.result));
    }

    private void OnItemMissed(ItemMissedEvent e)
    {
        string warning = FeedbackMessages.Warning(e.item, e.result, e.attractsAnimals);
        Show("Missed!", FeedbackMessages.Body(e.item), warning, ColourFor(e.result));
    }

    private void OnBinFull(BinFullEvent e)
    {
        Show("Bin full", FeedbackMessages.BinFull(e.bin), string.Empty, mistakeColour);
    }

    private void OnBinRejected(BinRejectedEvent e)
    {
        Show("Batch rejected", FeedbackMessages.BinRejected(e.bin), string.Empty, seriousColour);
    }

    private Color ColourFor(SortResult result)
    {
        if (result.correct)
        {
            return correctColour;
        }

        return result.severity == ErrorSeverity.Serious ? seriousColour : mistakeColour;
    }

    private void Show(string title, string body, string warning, Color colour)
    {
        titleText.text = title;
        bodyText.text = body;
        warningText.text = warning;
        warningText.gameObject.SetActive(warning.Length > 0);
        background.color = colour;
        panel.SetActive(true);
        hideTimer = showSeconds;
    }
}
