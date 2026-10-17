using System.Collections.Generic;
using Unity.Loading;
using UnityEngine;

public class ScenarioLoader : MonoBehaviour
{
    [SerializeField] private bool logContentEvents;

    private readonly Dictionary<string, GameObject> loadedModels = new Dictionary<string, GameObject>();
    private readonly ScenarioContentDirectory directory = new ScenarioContentDirectory();
    private ScenarioContent loadedContent;

    public bool IsLoaded => loadedContent != null;
    public IReadOnlyDictionary<string, GameObject> Models => loadedModels;

    public async Awaitable<bool> LoadAsync(string scenarioId)
    {
        Release();
        ScenarioContent content = directory.Find(scenarioId);

        if (content == null)
        {
            Log("No content found for " + scenarioId);
            return false;
        }

        foreach (ItemModelEntry entry in content.Models)
        {
            await entry.Model.LoadAsync();

            if (entry.Model.Status == LoadableStatus.Loaded)
            {
                loadedModels[entry.ItemId] = entry.Model.Target;
            }
        }

        loadedContent = content;
        Log("Loaded " + scenarioId + ": " + loadedModels.Count + " of " + content.Models.Count + " models");
        return loadedModels.Count == content.Models.Count;
    }

    public void Release()
    {
        if (loadedContent == null)
        {
            return;
        }

        foreach (ItemModelEntry entry in loadedContent.Models)
        {
            entry.Model.Release();
        }

        Log("Released " + loadedContent.ScenarioId + ": " + loadedModels.Count + " models");
        loadedModels.Clear();
        loadedContent = null;
    }

    private void OnDestroy()
    {
        Release();
        directory.Unregister();
    }

    private void Log(string message)
    {
        if (logContentEvents)
        {
            Debug.Log("[Content] " + message);
        }
    }
}
