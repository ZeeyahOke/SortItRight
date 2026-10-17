using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private WasteItemFactory factory;
    [SerializeField] private BeltSpawner spawner;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float baseSpeed = 0.05f;

    private readonly List<WasteItem> activeItems = new List<WasteItem>();
    private float speed;

    public bool IsRunning { get; private set; }

    public void StartBelt(ScenarioData scenario)
    {
        ClearItems();
        speed = baseSpeed;
        IsRunning = true;
        spawner.Begin(scenario);
    }

    public void StopBelt()
    {
        IsRunning = false;
        spawner.Stop();
        ClearItems();
    }

    public void IncreaseSpeed(float fraction)
    {
        speed *= 1f + fraction;
    }

    public WasteItem PlaceOnBelt(WasteItemData data)
    {
        WasteItem item = factory.Create(data, spawnPoint.position);
        item.transform.SetParent(transform, false);
        item.transform.SetLocalPositionAndRotation(spawnPoint.localPosition, Quaternion.identity);
        activeItems.Add(item);
        return item;
    }

    private void Update()
    {
        if (IsRunning)
        {
            MoveItems(speed * Time.deltaTime);
        }
    }

    private void MoveItems(float step)
    {
        Vector3 end = endPoint.localPosition;

        for (int i = activeItems.Count - 1; i >= 0; i--)
        {
            WasteItem item = activeItems[i];
            Transform itemTransform = item.transform;

            if (item.IsOnBelt)
            {
                itemTransform.localPosition = Vector3.MoveTowards(itemTransform.localPosition, end, step);
            }

            if (item.IsOnBelt && itemTransform.localPosition == end)
            {
                MarkMissed(item);
            }

            if (!item.IsOnBelt)
            {
                factory.Recycle(item);
                activeItems.RemoveAt(i);
            }
        }
    }

    private void MarkMissed(WasteItem item)
    {
        SortResult result = item.OnMissed();
        EventBus.Publish(new ItemMissedEvent(item.Data, result, item.AttractsAnimals));
        item.MarkResolved();
    }

    private void ClearItems()
    {
        for (int i = activeItems.Count - 1; i >= 0; i--)
        {
            factory.Recycle(activeItems[i]);
        }

        activeItems.Clear();
    }
}
