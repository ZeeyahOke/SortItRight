using System;
using Unity.Loading;
using UnityEngine;

[Serializable]
public class ItemModelEntry
{
    [SerializeField] private string itemId;
    [SerializeField] private Loadable<GameObject> model;

    public string ItemId => itemId;
    public Loadable<GameObject> Model => model;
}
