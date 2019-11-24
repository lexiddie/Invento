namespace Invento.Dtos
{
    public class MeasurementDto
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string Abbreviation { get; set; }

        public string Description { get; set; }
        
        public bool IsActive { get; set; }
        
        public string CreatedAt { get; set; }

        public string CreatedBy { get; set; }
    }
}