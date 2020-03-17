using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tarim.Api.Infrastructure.Interface;

namespace Tarim.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
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
    }
}
