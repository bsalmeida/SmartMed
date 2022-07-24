using System;

namespace SmartMed.Domain
{
    public class Medication
    {
        public Medication(string name, int quantity)
        {
            this.Name = name;
            this.Quantity = quantity > 0 ? quantity : throw new InvalidOperationException("Quantity must be greater than zero.");
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
