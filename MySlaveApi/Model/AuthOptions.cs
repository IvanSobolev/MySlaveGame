using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MySlaveApi.Model;

public class AuthOptions
{
    public const string ISSUER = "MySlaveGameAPI";
    public const string AUDIENCE = "GameUser";
    const string KEY = "mysupersecret_secretsecretsecretkey!123";
    
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
       return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
        
}