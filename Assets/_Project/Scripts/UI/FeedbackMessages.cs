public static class FeedbackMessages
{
    public static string SortTitle(SortResult result)
    {
        if (result.correct)
        {
            return "Correct!";
        }

        return result.severity == ErrorSeverity.Serious ? "Serious mistake" : "Not quite";
    }

    public static string Body(WasteItemData item)
    {
        return item.DisplayName + " goes in the " + item.CorrectBin + " bin. " + item.Reason;
    }

    public static string Warning(WasteItemData item, SortResult result, bool attractsAnimals)
    {
        if (attractsAnimals)
        {
            return "Food left out attracts rats, flies and stray animals.";
        }

        if (result.severity != ErrorSeverity.Serious)
        {
            return string.Empty;
        }

        if (item.IsSharp)
        {
            return "Injury risk: sharp items can cut the people who collect the waste.";
        }

        return "Danger: this can harm people and the environment if it is not handled safely.";
    }

    public static string BinFull(BinType bin)
    {
        return "The " + bin + " bin is full. Tap it to empty it.";
    }

    public static string BinRejected(BinType bin)
    {
        return "Too many wrong items. The " + bin + " batch was rejected.";
    }
}
