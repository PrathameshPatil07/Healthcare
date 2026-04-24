using Dapper;
using Healthcare.DAL.Data;
using Healthcare.DAL.Models;
using Healthcare.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.DAL.Repository.Implementation
{
    public class PatientRepository: IPatientRepository
    {
        private readonly DapperContext _context;

        public PatientRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<int> InsertPatient(Patient patient)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", patient.FirstName);
            parameters.Add("@LastName", patient.LastName);
            parameters.Add("@PhoneNumber", patient.PhoneNumber);
            parameters.Add("@DateOfBirth", patient.DateOfBirth);
            parameters.Add("@Gender", (int)patient.Gender);

            using var connection = _context.CreateConnection();

            return await connection.ExecuteScalarAsync<int>(
                "sp_InsertPatient",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            using var connection = _context.CreateConnection();

            return await connection.QueryAsync<Patient>(
                "sp_GetAllPatients",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Patient>> GetPatientByFirstName(string firstName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", firstName);

            using var connection = _context.CreateConnection();

            return await connection.QueryAsync<Patient>(
                "sp_GetPatientByFirstName",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Patient>> GetPatientByName(string firstName, string lastName)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", firstName);
            parameters.Add("@LastName", lastName);

            using var connection = _context.CreateConnection();

            return await connection.QueryAsync<Patient>(
                "sp_GetPatientByName",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
