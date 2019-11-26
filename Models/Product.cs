namespace Invento.Models
{
    public class Product
    {
        public string Id { get; set; }
        
        public string Category { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }
        
        public bool IsActive { get; set; }
        
        public string CreatedDate { get; set; }

        public string CreatedTime { get; set; }

        public string CreatedBy { get; set; }
    }
}