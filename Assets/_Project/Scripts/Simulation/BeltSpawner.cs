using System.Collections.Generic;
using UnityEngine;

public class BeltSpawner : MonoBehaviour
{
    [SerializeField] private ConveyorBelt belt;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spacing = 0.12f;

    private readonly List<WasteItemData> sequence = new List<WasteItemData>();
    private int nextIndex;
    private WasteItem lastItem;

    public int SpawnedCount => nextIndex;
    public int TotalCount => sequence.Count;

    public void Begin(ScenarioData scenario)
    {
        SessionItemSequence.Build(scenario.ItemPool, scenario.SessionItemCount, sequence);
        nextIndex = 0;
        lastItem = null;
    }

    public void Stop()
    {
        sequence.Clear();
        nextIndex = 0;
        lastItem = null;
    }

    private void Update()
    {
        if (belt.IsRunning && nextIndex < sequence.Count && HasRoom())
        {
            SpawnNext();
        }
    }

    private bool HasRoom()
    {
        if (lastItem == null || !lastItem.IsOnBelt)
        {
            return true;
        }

        float travelled = Vector3.Distance(lastItem.transform.localPosition, spawnPoint.localPosition);
        return travelled >= spacing;
    }

    private void SpawnNext()
    {
        lastItem = belt.PlaceOnBelt(sequence[nextIndex]);
        nextIndex++;
    }
}
