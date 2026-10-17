using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioContent_", menuName = "Sort It Right/Scenario Content")]
public class ScenarioContent : ScriptableObject
{
    [SerializeField] private string scenarioId;
    [SerializeField] private List<ItemModelEntry> models = new List<ItemModelEntry>();

    public string ScenarioId => scenarioId;
    public IReadOnlyList<ItemModelEntry> Models => models;
}
