using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using revanelxan.Data;
using revanelxan.Dtos;
using revanelxan.Models;
using revanelxan.VM;
using System.Security.Claims;

namespace revanelxan.Controllers
{
    [Authorize]
    public class AddController : Controller
    {
        private readonly MyDbContext _context;
        public AddController(MyDbContext context)
        {

            _context = context;
        }
        public IActionResult Add()
        {
            AddVm vm = new AddVm() { Picture = _context.Pictures.FirstOrDefault(x => x.Id == 4),
                Categories = _context.Categories.ToList()
                    
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult Add(AddVm pr)
        {
            if (pr.Product == null) return NotFound();
            pr.Product.Status = true;
            _context.Products.Add(pr.Product);
            _context.SaveChanges();

            return RedirectToAction("AddList");
        }
        public IActionResult AddList()
        {
            ViewBag.rol = User.FindFirst(ClaimTypes.Role)?.Value;
            var ass= from pr in _context.Products
            join ct in _context.Categories on pr.CategoryID equals ct.Id
            select new AddListDto
            {
                Id =pr.Id,
                Name=pr.Name,
                Category=ct.Name,
                Barcode=pr.Barcode,
                CategoryId=ct.Id,
                Cost=pr.Cost,
                Count=pr.Count
            };
            AddListVm vm = new AddListVm() { Products=ass };
            return View(vm);

        }
        public IActionResult Adddelete(int id)
        {
            if (id == null) return NotFound();
            var deleteitem = _context.Products.FirstOrDefault(x => x.Id == id  );
            if (deleteitem == null) return NotFound();
            _context.Products.Remove(deleteitem);
            _context.SaveChanges();
            return RedirectToAction("AddList", "Add");
        }
        public IActionResult AddUpdate(int id)
        {
            if (id == null) return NotFound();
            var getitem = _context.Products.FirstOrDefault(x => x.Id == id );
            
            if (getitem == null) return NotFound();

            AddUpdateVm vm = new AddUpdateVm() { productget=getitem ,
            singlecategory=_context.Categories.FirstOrDefault(x=>x.Id==getitem.CategoryID),
            Categories=_context.Categories.ToList(),
                Picture = _context.Pictures.FirstOrDefault(x => x.Id == 4)
            };
            return View(vm);

        }
        [HttpPost]
        public IActionResult AddUpdate(Product pr,int id)
        {
            if (pr == null) return NotFound();
            var ui = _context.Products.FirstOrDefault(x => x.Id == id);
            if (ui == null) return NotFound();
            ui.Name = pr.Name;
            ui.CategoryID = pr.CategoryID;
            ui.Barcode = pr.Barcode;
            ui.Cost = pr.Cost;
            ui.Count = pr.Count;
            ui.EndTime = pr.EndTime;
            ui.Status = pr.Status;
            _context.Products.Update(ui);
            return RedirectToAction("addlist", "home");

        }

    }
}
