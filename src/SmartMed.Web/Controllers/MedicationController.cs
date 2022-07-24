using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartMed.Data;
using SmartMed.Domain;
using SmartMed.Web.DTO;

namespace SmartMed.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicationController : ControllerBase
    {
        private readonly ILogger<MedicationController> logger;
        private readonly IMedicationRepository medicationRepository;

        public MedicationController(ILogger<MedicationController> logger, IMedicationRepository medicationRepository)
        {
            this.logger = logger;
            this.medicationRepository = medicationRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMedicationsAsync()
        {
            var result = await medicationRepository.GetAllMedicationAsync();

            logger.LogInformation("Medication obtained.");

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateMedicationAsync([FromBody] MedicationDto medicationDto)
        {
            var medication = new Medication(medicationDto.Name, medicationDto.Quantity);
            var result = await medicationRepository.CreateMedicationAsync(medication);

            logger.LogInformation($"Medication {medication.Name} created with {medication.Quantity} units.");

            return this.Created(string.Empty, result);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteMedicationAsync([FromRoute] Guid id)
        {
            var result = await medicationRepository.DeleteMedicationAsync(id);

            if (result)
            {
                logger.LogInformation($"Medication {id} deleted.");

                return this.NoContent();
            }

            var message = $"Cannot delete medication {id}.";
            logger.LogError(message);

            return this.BadRequest(message);
        }
    }
}
