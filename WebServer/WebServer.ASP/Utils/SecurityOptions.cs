using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebServer.ASP.Utils;

public static class SecurityOptions
{
    public const string Issuer = "ServerPhotostudio";
    public const string Audience = "ClientPhotostudio";
    private const string Key = "54b6ec373e54f349d4d769474a55673a34b2ec704189d2bc22c3963501a0a3a5";
    public static SymmetricSecurityKey GetSymmetricSecurityKey => new(Encoding.UTF8.GetBytes(Key));
    public static readonly SigningCredentials SigningCredentials = new(GetSymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
}