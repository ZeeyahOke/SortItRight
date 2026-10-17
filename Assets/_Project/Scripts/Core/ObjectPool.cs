using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private readonly Stack<T> available = new Stack<T>();
    private readonly T prefab;
    private readonly Transform parent;

    public ObjectPool(T prefab, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;
    }

    public int AvailableCount => available.Count;

    public T Get()
    {
        T item = available.Count > 0 ? available.Pop() : CreateNew();
        item.gameObject.SetActive(true);
        return item;
    }

    public void Release(T item)
    {
        item.gameObject.SetActive(false);
        item.transform.SetParent(parent, false);
        available.Push(item);
    }

    public void Prewarm(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Release(CreateNew());
        }
    }

    private T CreateNew()
    {
        return Object.Instantiate(prefab, parent);
    }
}
