namespace revanelxan.Dtos
{
    public class StoceDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Category { get; set; }

        public int Barcode { get; set; }
        public int Cost { get; set; }

        public int Count { get; set; }

       
        public DateTime EndTime { get; set; }
        public int RestOfDay { get; set; }
        public bool Status { get; set; }
    }
}
