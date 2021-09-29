namespace Invento.Dtos
{
    public class ProductDto
    {
        public string Id { get; set; }
        
        public string CategoryId { get; set; }
        
        public string MeasurementId { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }
        
        public bool IsActive { get; set; }
        
        public string CreatedAt { get; set; }

        public string CreatedBy { get; set; }
    }
}