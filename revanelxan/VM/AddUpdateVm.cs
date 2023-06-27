using revanelxan.Models;

namespace revanelxan.VM
{
    public class AddUpdateVm
    {
        public Product productget { get; set; }
        public Category singlecategory { get; set; }
        public Product productpost { get; set; }

        public List<Category> Categories { get; set; }
        public Picture Picture { get; set; }
    }
}
