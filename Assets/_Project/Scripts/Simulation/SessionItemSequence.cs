using System.Collections.Generic;
using UnityEngine;

public static class SessionItemSequence
{
    public static void Build(IReadOnlyList<WasteItemData> pool, int count, List<WasteItemData> result)
    {
        result.Clear();

        for (int i = 0; i < pool.Count && result.Count < count; i++)
        {
            result.Add(pool[i]);
        }

        while (result.Count < count)
        {
            result.Add(pool[Random.Range(0, pool.Count)]);
        }

        Shuffle(result);
    }

    private static void Shuffle(List<WasteItemData> items)
    {
        for (int i = items.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (items[i], items[j]) = (items[j], items[i]);
        }
    }
}
