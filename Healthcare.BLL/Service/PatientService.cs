using FluentValidation;
using Healthcare.DAL.Models;
using Healthcare.DAL.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Healthcare.BLL.Service
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repository;
        private readonly IValidator<Patient> _validator;

        public PatientService(IPatientRepository repository, IValidator<Patient> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<int> CreatePatient(Patient patient)
        {
            // 🔍 Validation
            var validationResult = await _validator.ValidateAsync(patient);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new Exception(errors);
            }

            // 👉 Call repository
            return await _repository.InsertPatient(patient);
        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await _repository.GetAllPatients();
        }

        public async Task<IEnumerable<Patient>> GetPatientByName(string firstName, string lastName)
        {
            return await _repository.GetPatientByName(firstName, lastName);
        }
    }
}
