using System.Collections.Generic;

public class SessionStats
{
    private const int PointsPerCorrect = 10;
    private const int PenaltyPerMistake = 5;
    private const int PenaltyPerSeriousError = 15;
    private const float PassAccuracy = 0.8f;
    private const string MissedLabel = "Missed";

    private readonly List<MistakeRecord> mistakes = new List<MistakeRecord>();

    public int Correct { get; private set; }
    public int Incorrect { get; private set; }
    public int Missed { get; private set; }
    public int SeriousErrors { get; private set; }
    public int RejectedBins { get; private set; }
    public int ItemsHandled => Correct + Incorrect + Missed;
    public IReadOnlyList<MistakeRecord> Mistakes => mistakes;

    public void Reset()
    {
        Correct = 0;
        Incorrect = 0;
        Missed = 0;
        SeriousErrors = 0;
        RejectedBins = 0;
        mistakes.Clear();
    }

    public void RecordSort(string itemId, BinType placedIn, SortResult result)
    {
        if (result.WasRefused)
        {
            return;
        }

        if (result.correct)
        {
            Correct++;
            return;
        }

        Incorrect++;
        RecordMistake(itemId, placedIn.ToString(), result);
    }

    public void RecordMiss(string itemId, SortResult result)
    {
        Missed++;
        RecordMistake(itemId, MissedLabel, result);
    }

    public void RecordRejectedBin()
    {
        RejectedBins++;
    }

    public float Accuracy()
    {
        return ItemsHandled == 0 ? 0f : (float)Correct / ItemsHandled;
    }

    public int Score()
    {
        int normalMistakes = Incorrect + Missed - SeriousErrors;
        return Correct * PointsPerCorrect - normalMistakes * PenaltyPerMistake - SeriousErrors * PenaltyPerSeriousError;
    }

    public SessionOutcome Outcome(bool failRuleHit)
    {
        if (failRuleHit)
        {
            return SessionOutcome.Failed;
        }

        bool passed = Accuracy() >= PassAccuracy && SeriousErrors == 0;
        return passed ? SessionOutcome.Passed : SessionOutcome.Completed;
    }

    private void RecordMistake(string itemId, string placedIn, SortResult result)
    {
        if (result.severity == ErrorSeverity.Serious)
        {
            SeriousErrors++;
        }

        mistakes.Add(new MistakeRecord(itemId, placedIn, result.correctBin.ToString()));
    }
}
