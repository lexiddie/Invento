namespace Invento.Dtos
{
    public class LeftoverDto
    {
        public string Id { get; set; }

        public string Code { get; set; }

        public string ProductId { get; set; }
        
        public int Quantity { get; set; }
        
        public string Description { get; set; }

        public bool IsVoid { get; set; }
        
        public string CreatedAt { get; set; }

        public string CreatedBy { get; set; }
    }
}