using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scenario_", menuName = "Sort It Right/Scenario Data")]
public class ScenarioData : ScriptableObject
{
    [SerializeField] private string scenarioId;
    [SerializeField] private string displayName;
    [SerializeField] private int sessionItemCount = 30;
    [SerializeField] private List<WasteItemData> itemPool = new List<WasteItemData>();

    public string ScenarioId => scenarioId;
    public string DisplayName => displayName;
    public int SessionItemCount => sessionItemCount;
    public IReadOnlyList<WasteItemData> ItemPool => itemPool;
}
