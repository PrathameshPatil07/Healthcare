using Healthcare.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.BLL.Service
{
    public interface IPatientService
    {
        Task<int> CreatePatient(Patient patient);

        Task<IEnumerable<Patient>> GetAllPatients();

        Task<IEnumerable<Patient>> GetPatientByName(string firstName, string lastName);
    }
}
