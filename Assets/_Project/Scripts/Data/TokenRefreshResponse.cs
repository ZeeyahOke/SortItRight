using System;

[Serializable]
public class TokenRefreshResponse
{
    public string user_id;
    public string id_token;
    public string refresh_token;
    public string expires_in;
}
