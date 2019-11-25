namespace Invento.Models
{
    public class Supplier
    {
        public string Id { get; set; }

        public string Name { get; set; }
        
        public string Email { get; set; }

        public string TelephoneNumber { get; set; }

        public string Description { get; set; }
        
        public bool IsActive { get; set; }
        
        public string CreatedDate { get; set; }

        public string CreatedTime { get; set; }

        public string CreatedBy { get; set; }
    }
}