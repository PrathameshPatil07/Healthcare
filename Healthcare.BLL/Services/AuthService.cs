using Hospital.API.Models;
using Hospital.BLL.DTOs.Auth;
using Hospital.BLL.Interfaces;
using Hospital.DAL.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Hospital.API.Helpers;

namespace Hospital.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthService(
            IAuthRepository repository,
            IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<string> RegisterAsync(RegisterRequestDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = dto.RoleId
            };

            await _repository.RegisterAsync(user);

            return "User Registered Successfully";
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var user = await _repository.LoginAsync(dto.Email);

            if (user == null)
            {
                throw new Exception("Invalid Email");
            }

            bool isPasswordValid =
                BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new Exception("Invalid Password");
            }

            var token = JwtHelper.GenerateToken(
                user.UserId,
                user.Email,
                _configuration);

            return new LoginResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                Token = token
            };
        }
    }
}
