using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tarim.Api.Infrastructure.Common;
using Tarim.Api.Infrastructure.Interface;
using Tarim.Api.Infrastructure.Model;
using Tarim.Api.Infrastructure.Model.Auth;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Tarim.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> FindUser(string email)
        {
            var result = await _userRepository.FindUser(email);
            return Ok(result.Object);
        }

        [AllowAnonymous]
        [Route("token")]
        [HttpPost]
        public async Task<IActionResult> GoogleAuthenticate([FromBody] GoogleUserRequest request)
        {
            if (ModelState.IsValid)
            {
                return Ok(await AuthenticateGoogleUser(request));
            }

            return BadRequest(ModelState.Values.SelectMany(it => it.Errors).Select(it => it.ErrorMessage));
        }

        private async Task<User> AuthenticateGoogleUser(GoogleUserRequest request)
        {
            Payload payload = await ValidateAsync(request.IdToken, new ValidationSettings
            {
                Audience = new[]
                {
                    Startup.StaticConfig["Auth:Google:ClientId"]
                }
            });
            if (payload.EmailVerified) {
                var user = await _userRepository.FindUser(payload.Email);
                if (user.Status == Infrastructure.Common.Enums.ExecuteStatus.Success)
                {
                    user.Object.Token = request.AccessToken;
                    return user.Object;
                }
            }
            return null;
        }
    }
}
