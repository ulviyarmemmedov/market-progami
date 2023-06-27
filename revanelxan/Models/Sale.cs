namespace revanelxan.Models
{
    public class Sale
    {
        public int Id { get; set; }
      
        public int CategoryID { get; set; }

        public int Name { get; set; }
        public int Barcode { get; set; }
        public int Cost { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
    }
}
