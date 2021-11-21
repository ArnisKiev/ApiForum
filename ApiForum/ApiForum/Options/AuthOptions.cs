using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ApiForum.Options
{
    public class AuthOptions
    {
        public const string ISSUER = "Forum";
        public const string AUDIENCE = "ForumUsers";
        public const string KEY = "ForumSecurityKey";
        public const int LIFETIME = 30;
        public static SymmetricSecurityKey GetSymmetSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.Default.GetBytes(KEY));
        }
    }
}
