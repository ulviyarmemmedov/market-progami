using revanelxan.Models;

namespace revanelxan.Dtos
{
    public class AddListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }

        public int Barcode { get; set; }
        public int Cost { get; set; }
        public int Count { get; set; }
        public bool Status { get; set; }
    }
}
