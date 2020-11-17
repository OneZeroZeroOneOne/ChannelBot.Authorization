using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChannelBot.BLL.Abstractions;
using ChannelBot.BLL.Services;
using ChannelBot.DAL.ViewModel.Out;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace ChannelBot.Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IAuthService _authService;
        public TokenController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        async public Task<ResponceAuthModel> Get(string email, string password)
        {

            var identity = await _authService.GetIdentity(email, password);

            var encodedJwt = _authService.GenerateToken(identity);

            return new ResponceAuthModel
            {
                Token = encodedJwt,
            };
        }
    }
}