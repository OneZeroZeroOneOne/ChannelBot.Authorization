using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelBot.BLL.Options
{
    public class AuthOptions
    {
        public string ISSUER; // издатель токена
        public string AUDIENCE; // потребитель токена
        public string KEY;   // ключ для шифрации
        public const int LIFETIME = 60; // время жизни токена - 1 минута

        public AuthOptions(string key, string issuer, string audience)
        {
            ISSUER = issuer;
            AUDIENCE = audience;
            KEY = key;
        }
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
