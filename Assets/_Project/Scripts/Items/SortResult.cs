public readonly struct SortResult
{
    public readonly bool correct;
    public readonly ErrorSeverity severity;
    public readonly BinType correctBin;

    public SortResult(bool correct, ErrorSeverity severity, BinType correctBin)
    {
        this.correct = correct;
        this.severity = severity;
        this.correctBin = correctBin;
    }

    public static SortResult Correct(BinType bin)
    {
        return new SortResult(true, ErrorSeverity.None, bin);
    }

    public static SortResult Wrong(BinType correctBin, ErrorSeverity severity)
    {
        return new SortResult(false, severity, correctBin);
    }
}
