using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BarleyBreakApi
{
    public class AuthOptions
    {
        public const string ISSUER = "dotNetServer";
        public const string AUDIENCE = "AngularClient";
        const string KEY = "mysupersecret_secretkey!123";
        public const int LIFETIME = 240;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
