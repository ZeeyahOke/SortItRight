using System.Collections.Generic;

public interface ISessionRepository
{
    void Save(SessionRecord record);

    IReadOnlyList<SessionRecord> LoadRecent(int count);
}
