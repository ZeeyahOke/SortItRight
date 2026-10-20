using System.Globalization;
using UnityEngine;
using UnityEngine.Networking;

public class FirebaseAuthClient
{
    private const string RefreshTokenKey = "FirebaseRefreshToken";
    private const string SignUpUrl = "https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=";
    private const string RefreshUrl = "https://securetoken.googleapis.com/v1/token?key=";
    private const float ExpiryMarginSeconds = 60f;

    private readonly string apiKey;
    private float expiresAt;

    public FirebaseAuthClient(string apiKey)
    {
        this.apiKey = apiKey;
    }

    public string UserId { get; private set; }
    public string IdToken { get; private set; }
    public bool IsSignedIn => !string.IsNullOrEmpty(IdToken) && Time.realtimeSinceStartup < expiresAt;

    public async Awaitable<bool> SignInAsync()
    {
        if (IsSignedIn)
        {
            return true;
        }

        string refreshToken = PlayerPrefs.GetString(RefreshTokenKey, string.Empty);

        if (refreshToken.Length > 0 && await RefreshAsync(refreshToken))
        {
            return true;
        }

        return await SignUpAsync();
    }

    private async Awaitable<bool> SignUpAsync()
    {
        string body = "{\"returnSecureToken\":true}";
        string json = await FirebaseHttp.SendAsync("POST", SignUpUrl + apiKey, body, "application/json", null);

        if (json == null)
        {
            return false;
        }

        SignUpResponse response = JsonUtility.FromJson<SignUpResponse>(json);
        Store(response.localId, response.idToken, response.refreshToken, response.expiresIn);
        return true;
    }

    private async Awaitable<bool> RefreshAsync(string refreshToken)
    {
        string body = "grant_type=refresh_token&refresh_token=" + UnityWebRequest.EscapeURL(refreshToken);
        string json = await FirebaseHttp.SendAsync("POST", RefreshUrl + apiKey, body, "application/x-www-form-urlencoded", null);

        if (json == null)
        {
            return false;
        }

        TokenRefreshResponse response = JsonUtility.FromJson<TokenRefreshResponse>(json);
        Store(response.user_id, response.id_token, response.refresh_token, response.expires_in);
        return true;
    }

    private void Store(string userId, string idToken, string refreshToken, string expiresIn)
    {
        UserId = userId;
        IdToken = idToken;
        expiresAt = Time.realtimeSinceStartup + float.Parse(expiresIn, CultureInfo.InvariantCulture) - ExpiryMarginSeconds;
        PlayerPrefs.SetString(RefreshTokenKey, refreshToken);
        PlayerPrefs.Save();
    }
}
