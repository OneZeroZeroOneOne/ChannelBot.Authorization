using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ChannelBot.BLL.Abstractions
{
    public interface IAuthService
    {
        public Task<ClaimsIdentity> GetIdentity(string login, string password);
        public string GenerateToken(ClaimsIdentity claims);
    }
}
