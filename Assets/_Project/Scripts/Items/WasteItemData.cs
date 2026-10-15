using UnityEngine;

[CreateAssetMenu(fileName = "Item_", menuName = "Sort It Right/Waste Item Data")]
public class WasteItemData : ScriptableObject
{
    [SerializeField] private string itemId;
    [SerializeField] private string displayName;
    [SerializeField] private string material;
    [SerializeField] private BinType correctBin;
    [SerializeField] private ItemCategory category;
    [SerializeField] private bool isSharp;
    [SerializeField] private bool isTricky;
    [SerializeField, TextArea] private string hint;
    [SerializeField, TextArea] private string reason;

    public string ItemId => itemId;
    public string DisplayName => displayName;
    public string MaterialName => material;
    public BinType CorrectBin => correctBin;
    public ItemCategory Category => category;
    public bool IsSharp => isSharp;
    public bool IsTricky => isTricky;
    public string Hint => hint;
    public string Reason => reason;
}
