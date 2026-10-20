using System;

[Serializable]
public class LeaderboardFields
{
    public FirestoreValue nickname;
    public FirestoreValue playerId;
    public FirestoreValue scenarioId;
    public FirestoreValue score;
    public FirestoreValue dateUtc;
}
