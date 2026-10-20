using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public static class FirebaseHttp
{
    private const int TimeoutSeconds = 10;

    public static async Awaitable<string> SendAsync(string method, string url, string body, string contentType, string idToken)
    {
        using UnityWebRequest request = new UnityWebRequest(url, method);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.timeout = TimeoutSeconds;

        if (body != null)
        {
            request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(body));
            request.SetRequestHeader("Content-Type", contentType);
        }

        if (!string.IsNullOrEmpty(idToken))
        {
            request.SetRequestHeader("Authorization", "Bearer " + idToken);
        }

        await request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning("Firebase request failed: " + request.error);
            return null;
        }

        return request.downloadHandler.text;
    }
}
