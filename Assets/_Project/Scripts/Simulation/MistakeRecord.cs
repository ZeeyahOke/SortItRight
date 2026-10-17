using System;

[Serializable]
public class MistakeRecord
{
    public string itemId;
    public string placedIn;
    public string correctBin;

    public MistakeRecord(string itemId, string placedIn, string correctBin)
    {
        this.itemId = itemId;
        this.placedIn = placedIn;
        this.correctBin = correctBin;
    }
}
