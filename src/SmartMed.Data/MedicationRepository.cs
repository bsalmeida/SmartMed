using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using SmartMed.Domain;

namespace SmartMed.Data
{
    internal class MedicationRepository : IMedicationRepository
    {
        private readonly IMongoCollection<Medication> medicationCollection;

        public MedicationRepository(IMongoClient client)
        {
            medicationCollection = client.GetDatabase("SVC_MEDICATIONS").GetCollection<Medication>("Medications");
        }

        public async Task<IEnumerable<Medication>> GetAllMedicationAsync()
        {
            var query = await medicationCollection.Find(Builders<Medication>.Filter.Empty).ToListAsync();

            return query;
        }

        public async Task<Medication> CreateMedicationAsync(Medication medication)
        {
            await medicationCollection.InsertOneAsync(medication);

            return medication;
        }

        public async Task<bool> DeleteMedicationAsync(Guid id)
        {
            var deleteFilter = Builders<Medication>.Filter.Eq(x => x.Id, id);

            var result = await medicationCollection.DeleteOneAsync(deleteFilter);

            return result.IsAcknowledged && result.DeletedCount == 1;
        }
    }
}
