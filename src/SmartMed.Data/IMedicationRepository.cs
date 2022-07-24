using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartMed.Domain;

namespace SmartMed.Data
{
    public interface IMedicationRepository
    {
        public Task<IEnumerable<Medication>> GetAllMedicationAsync();
        public Task<Medication> CreateMedicationAsync(Medication medication);
        Task<bool> DeleteMedicationAsync(Guid id);
    }
}
