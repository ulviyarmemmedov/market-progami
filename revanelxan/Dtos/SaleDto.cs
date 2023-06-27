namespace revanelxan.Dtos
{
    public class SaleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
       
        public string Category { get; set; }

        public int Barcode { get; set; }
        public int Cost { get; set; }

        public int Count { get; set; }

        public int TotalCount { get; set; }

        public DateTime SaleDate { get; set; }
       

    }
}
