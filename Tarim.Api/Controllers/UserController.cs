using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
                return await AuthenticateGoogleUser(request);
            }

            return BadRequest(ModelState.Values.SelectMany(it => it.Errors).Select(it => it.ErrorMessage));
        }

        private async Task<IActionResult> AuthenticateGoogleUser(GoogleUserRequest request)
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
                    user.Object.Token = GenerateUserToken(user.Object);// request.AccessToken;
                    return Ok(user.Object);
                }
            }
            return Unauthorized();
        }

        #region Private Methods
        private string GenerateUserToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(Startup.StaticConfig["Auth:Google:ClientSecret"]);

            var expires = DateTime.UtcNow.AddDays(7);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Email) ,
                    new Claim(JwtRegisteredClaimNames.Sub, Startup.StaticConfig["Auth:Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Surname, user.FirstName),
                    new Claim(ClaimTypes.GivenName, user.LastName),
                    new Claim(ClaimTypes.NameIdentifier, user.Email),
                    new Claim(ClaimTypes.Role,user.Profile.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),

                Expires = expires,

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = Startup.StaticConfig["Auth:Jwt:Issuer"],
                Audience = Startup.StaticConfig["Auth:Jwt:Audience"]
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }
        #endregion
    }
}
