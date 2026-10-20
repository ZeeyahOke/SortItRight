using TMPro;
using UnityEngine;

public class BinStrikeChip : MonoBehaviour
{
    [SerializeField] private TMP_Text strikesText;

    private int shownStrikes = -1;

    public void SetStrikes(int strikes, int maxStrikes)
    {
        if (strikes == shownStrikes)
        {
            return;
        }

        shownStrikes = strikes;
        strikesText.SetText("{0}/{1}", (float)strikes, maxStrikes);
    }
}
