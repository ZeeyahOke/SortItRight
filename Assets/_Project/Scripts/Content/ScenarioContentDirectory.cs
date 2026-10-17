using System.IO;
using Unity.Loading;
using UnityEngine;

public class ScenarioContentDirectory
{
    private ContentDirectoryHandle handle;

    public ScenarioContent Find(string scenarioId)
    {
        if (!handle.IsValid)
        {
            handle = ContentLoadManager.RegisterContentDirectory(DirectoryPath());
        }

        foreach (ScenarioContent content in ContentLoadManager.GetRootAssets<ScenarioContent>(handle))
        {
            if (content.ScenarioId == scenarioId)
            {
                return content;
            }
        }

        return null;
    }

    public void Unregister()
    {
        if (handle.IsValid)
        {
            ContentLoadManager.UnregisterContentDirectory(handle);
            handle = default;
        }
    }

    private static string DirectoryPath()
    {
        if (Application.isEditor)
        {
            return ContentPaths.EditorOutputFolder;
        }

        return Path.Combine(Application.streamingAssetsPath, ContentPaths.StreamingFolder);
    }
}
