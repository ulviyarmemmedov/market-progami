namespace revanelxan.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }

        public int Barcode { get; set; }
        public int Cost { get; set; }
        public int Count { get; set; }
        public DateTime EndTime { get; set; }
        public bool Status { get; set; }
    }
}
