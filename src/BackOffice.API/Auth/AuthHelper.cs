using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Backoffice.API.Auth;

public static class AuthHelper
{
    public static RsaSecurityKey BuildRSAKey(string publicKeyJWT)
    {
        RSA rsa = RSA.Create();

        rsa.ImportSubjectPublicKeyInfo(

            source: Convert.FromBase64String(publicKeyJWT),
            bytesRead: out _
        );

        var IssuerSigningKey = new RsaSecurityKey(rsa);

        return IssuerSigningKey;
    }
}
