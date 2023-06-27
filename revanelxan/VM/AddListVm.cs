using revanelxan.Dtos;
using revanelxan.Models;

namespace revanelxan.VM
{
    public class AddListVm
    {
        public IQueryable<AddListDto> Products { get; set; }
    }
}
