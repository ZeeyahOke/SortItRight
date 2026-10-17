using System.Collections.Generic;
using UnityEngine;

public class WasteItemFactory : MonoBehaviour
{
    [SerializeField] private RecyclableItem recyclablePrefab;
    [SerializeField] private PerishableItem perishablePrefab;
    [SerializeField] private HazardousItem hazardousPrefab;
    [SerializeField] private int prewarmPerCategory = 6;

    private ObjectPool<RecyclableItem> recyclables;
    private ObjectPool<PerishableItem> perishables;
    private ObjectPool<HazardousItem> hazardous;
    private IReadOnlyDictionary<string, GameObject> models;

    private void Awake()
    {
        recyclables = new ObjectPool<RecyclableItem>(recyclablePrefab, transform);
        perishables = new ObjectPool<PerishableItem>(perishablePrefab, transform);
        hazardous = new ObjectPool<HazardousItem>(hazardousPrefab, transform);
        recyclables.Prewarm(prewarmPerCategory);
        perishables.Prewarm(prewarmPerCategory);
        hazardous.Prewarm(prewarmPerCategory);
    }

    public void SetModels(IReadOnlyDictionary<string, GameObject> loadedModels)
    {
        models = loadedModels;
    }

    public WasteItem Create(WasteItemData data, Vector3 position)
    {
        WasteItem item = GetFromPool(data.Category);
        item.transform.position = position;
        item.Initialise(data);
        item.ModelView.Show(FindModel(data.ItemId));
        return item;
    }

    public void Recycle(WasteItem item)
    {
        item.ResetForPool();

        if (item is RecyclableItem recyclable)
        {
            recyclables.Release(recyclable);
        }
        else if (item is PerishableItem perishable)
        {
            perishables.Release(perishable);
        }
        else if (item is HazardousItem hazardousItem)
        {
            hazardous.Release(hazardousItem);
        }
    }

    private GameObject FindModel(string itemId)
    {
        if (models != null && models.TryGetValue(itemId, out GameObject model))
        {
            return model;
        }

        return null;
    }

    private WasteItem GetFromPool(ItemCategory category)
    {
        if (category == ItemCategory.Perishable)
        {
            return perishables.Get();
        }

        if (category == ItemCategory.Hazardous)
        {
            return hazardous.Get();
        }

        return recyclables.Get();
    }
}
