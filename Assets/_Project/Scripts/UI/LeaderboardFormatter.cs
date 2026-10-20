public static class LeaderboardFormatter
{
    public static string Row(int rank, LeaderboardEntry entry)
    {
        return "<b>" + rank + ".</b>  <noparse>" + entry.nickname + "</noparse><pos=78%><b>" + entry.score + "</b>";
    }
}
