using System;

[Serializable]
public class SignUpResponse
{
    public string localId;
    public string idToken;
    public string refreshToken;
    public string expiresIn;
}
