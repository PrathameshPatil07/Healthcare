using Healthcare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.DAL.Repository.Interface
{
    public interface IPatientRepository
    {
        Task<int> InsertPatient(Patient patient);

        Task<IEnumerable<Patient>> GetAllPatients();

        Task<IEnumerable<Patient>> GetPatientByFirstName(string firstName);

        Task<IEnumerable<Patient>> GetPatientByName(string firstName, string lastName);
    }
}
