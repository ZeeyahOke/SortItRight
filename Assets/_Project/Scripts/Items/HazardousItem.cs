public class HazardousItem : WasteItem
{
    public bool IsSharp => Data.IsSharp;

    public override SortResult EvaluateSort(BinType bin)
    {
        if (bin == Data.CorrectBin)
        {
            return SortResult.Correct(bin);
        }

        if (IsSafeBin(bin))
        {
            return SortResult.Wrong(Data.CorrectBin, ErrorSeverity.Normal);
        }

        return SortResult.Wrong(Data.CorrectBin, ErrorSeverity.Serious);
    }

    public override SortResult OnMissed()
    {
        return SortResult.Wrong(Data.CorrectBin, ErrorSeverity.Serious);
    }

    private static bool IsSafeBin(BinType bin)
    {
        return bin == BinType.Hazardous || bin == BinType.Sharps;
    }
}
