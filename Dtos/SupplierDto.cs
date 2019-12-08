namespace Invento.Dtos
{
    public class SupplierDto
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public string Email { get; set; }

        public string TelephoneNumber { get; set; }
        
        public string Country { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }
        
        public bool IsActive { get; set; }
        
        public string CreatedAt { get; set; }

        public string CreatedBy { get; set; }
    }
}