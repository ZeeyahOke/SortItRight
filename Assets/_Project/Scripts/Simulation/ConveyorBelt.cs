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
    private bool isRunning;

    public float Speed => speed;
    public bool IsRunning => isRunning;

    public void StartBelt(ScenarioData scenario)
    {
        ClearItems();
        speed = baseSpeed;
        isRunning = true;
        spawner.Begin(scenario);
    }

    public void StopBelt()
    {
        isRunning = false;
        spawner.Stop();
        ClearItems();
    }

    public void IncreaseSpeed(float fraction)
    {
        speed *= 1f + fraction;
    }

    private void Update()
    {
        if (isRunning)
        {
            MoveItems(Time.deltaTime);
        }
    }

    private void MoveItems(float deltaTime)
    {
        Vector3 end = endPoint.localPosition;
        float step = speed * deltaTime;

        for (int i = activeItems.Count - 1; i >= 0; i--)
        {
            WasteItem item = activeItems[i];

            if (!item.IsOnBelt)
            {
                RemoveAt(i);
                continue;
            }

            Transform itemTransform = item.transform;
            itemTransform.localPosition = Vector3.MoveTowards(itemTransform.localPosition, end, step);
        }
    }

    public WasteItem PlaceOnBelt(WasteItemData data)
    {
        WasteItem item = factory.Create(data, spawnPoint.position);
        item.transform.SetParent(transform, false);
        item.transform.SetLocalPositionAndRotation(spawnPoint.localPosition, Quaternion.identity);
        activeItems.Add(item);
        return item;
    }

    private void RemoveAt(int index)
    {
        factory.Recycle(activeItems[index]);
        activeItems.RemoveAt(index);
    }

    private void ClearItems()
    {
        for (int i = activeItems.Count - 1; i >= 0; i--)
        {
            RemoveAt(i);
        }
    }
}
