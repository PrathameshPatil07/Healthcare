using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hospital.BLL.DTOs.Auth;
using Hospital.BLL.Interfaces;


namespace Hospital.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterRequestDto dto)
        {
            var result = await _service.RegisterAsync(dto);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            LoginRequestDto dto)
        {
            var result = await _service.LoginAsync(dto);

            return Ok(result);
        }
    }
}
