namespace Invento.Models
{
    public class Inventory
    {
        public string Code { get; set; }
        
        public string Product { get; set; }
        
        public string Measurement { get; set; }

        public int Quantity { get; set; }

        public int Usage { get; set; }

        public int Leftover { get; set; }

        public int Available { get; set; }
    }
}