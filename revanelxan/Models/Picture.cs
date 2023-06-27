namespace revanelxan.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public string PictureName { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
