using System;
using System.IO;
using UnityEngine;

[Serializable]
public class FirebaseConfig
{
    private const string FileName = "FirebaseConfig.json";

    public string apiKey;
    public string projectId;

    public bool IsValid => !string.IsNullOrEmpty(apiKey) && !string.IsNullOrEmpty(projectId);

    public static FirebaseConfig Load()
    {
        string path = Path.Combine(Application.streamingAssetsPath, FileName);

        try
        {
            if (File.Exists(path))
            {
                return JsonUtility.FromJson<FirebaseConfig>(File.ReadAllText(path));
            }
        }
        catch (Exception exception)
        {
            Debug.LogWarning("Firebase config could not be read: " + exception.Message);
        }

        return null;
    }
}
