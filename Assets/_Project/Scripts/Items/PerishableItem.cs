public class PerishableItem : WasteItem
{
    public override bool AttractsAnimals => true;

    public override SortResult EvaluateSort(BinType bin)
    {
        return CorrectOrNormalMistake(bin);
    }

    public override SortResult OnMissed()
    {
        return SortResult.Wrong(Data.CorrectBin, ErrorSeverity.Normal);
    }
}
