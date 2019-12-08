namespace Invento.Models
{
    public class Purchase
    {
        public string Id { get; set; }
        
        public string Product { get; set; }
            
        public string Measurement { get; set; }
        
        public string Supplier { get; set; }

        public string Code { get; set; }

        public int Quantity { get; set; }
        
        public double Price { get; set; }

        public bool IsVoid { get; set; }
        
        public string CreatedDate { get; set; }

        public string CreatedTime { get; set; }

        public string CreatedBy { get; set; }
    }
}