using Healthcare.BLL.Service;
using Healthcare.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        // ✅ Create Patient
        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] Patient patient)
        {
            try
            {
                var id = await _patientService.CreatePatient(patient);
                return Ok(new
                {
                    Message = "Patient created successfully",
                    PatientId = id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Error = ex.Message
                });
            }
        }

        // ✅ Get All Patients
        [HttpGet]
        public async Task<IActionResult> GetAllPatients()
        {
            var result = await _patientService.GetAllPatients();
            return Ok(result);
        }

        // ✅ Get Patient By Name
        [HttpGet("search")]
        public async Task<IActionResult> GetPatientByName(
            [FromQuery] string firstName,
            [FromQuery] string lastName)
        {
            var result = await _patientService.GetPatientByName(firstName, lastName);
            return Ok(result);
        }
    }
}
