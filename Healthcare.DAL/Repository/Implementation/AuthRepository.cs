using Hospital.API.Models;
using Hospital.DAL.Context;
using Hospital.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Hospital.DAL.Repository.Implementation
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DapperContext _context;

        public AuthRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> RegisterAsync(User user)
        {
            using var connection = _context.CreateConnection();

            var parameters = new
            {
                user.FullName,
                user.Email,
                user.PasswordHash,
                user.RoleId
            };

            return await connection.ExecuteAsync(
                "sp_User_Register",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<User> LoginAsync(string email)
        {
            using var connection = _context.CreateConnection();

            return await connection.QueryFirstOrDefaultAsync<User>(
                "sp_User_Login",
                new { Email = email },
                commandType: CommandType.StoredProcedure);
        }
    }

}
