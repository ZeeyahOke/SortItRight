using TMPro;
using UnityEngine;

public class BinView : MonoBehaviour
{
    [SerializeField] private Bin bin;
    [SerializeField] private Transform fillPivot;
    [SerializeField] private Renderer[] strikeMarkers;
    [SerializeField] private Material strikeOffMaterial;
    [SerializeField] private Material strikeOnMaterial;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private Color fullColour = new Color(1f, 0.6f, 0.1f);
    [SerializeField] private Color rejectedColour = new Color(0.9f, 0.15f, 0.15f);

    private int shownFill = -1;
    private int shownStrikes = -1;
    private BinState shownState = (BinState)(-1);

    private void Update()
    {
        if (bin.FillLevel != shownFill)
        {
            shownFill = bin.FillLevel;
            ShowFill();
        }

        if (bin.Strikes != shownStrikes)
        {
            shownStrikes = bin.Strikes;
            ShowStrikes();
        }

        if (bin.State != shownState)
        {
            shownState = bin.State;
            ShowState();
        }
    }

    private void ShowFill()
    {
        float fraction = Mathf.Clamp01((float)shownFill / bin.Capacity);
        fillPivot.localScale = new Vector3(Mathf.Max(fraction, 0.001f), 1f, 1f);
    }

    private void ShowStrikes()
    {
        for (int i = 0; i < strikeMarkers.Length; i++)
        {
            strikeMarkers[i].sharedMaterial = i < shownStrikes ? strikeOnMaterial : strikeOffMaterial;
        }
    }

    private void ShowState()
    {
        if (shownState == BinState.Full)
        {
            SetStatus("FULL\nTap to empty", fullColour);
        }
        else if (shownState == BinState.Rejected)
        {
            SetStatus("REJECTED", rejectedColour);
        }
        else
        {
            SetStatus(string.Empty, Color.white);
        }
    }

    private void SetStatus(string text, Color colour)
    {
        statusText.text = text;
        statusText.color = colour;
    }
}
