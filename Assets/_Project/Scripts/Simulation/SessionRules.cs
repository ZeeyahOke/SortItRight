public static class SessionRules
{
    public const int RejectedBinsToFail = 2;
    public const int SeriousErrorsToFail = 3;

    public static bool HasFailed(SessionStats stats)
    {
        return stats.RejectedBins >= RejectedBinsToFail || stats.SeriousErrors >= SeriousErrorsToFail;
    }

    public static bool IsComplete(SessionStats stats, int totalItems)
    {
        return stats.ItemsHandled >= totalItems;
    }
}
