using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using revanelxan.Data;
using revanelxan.Dtos;
using revanelxan.Models;
using revanelxan.VM;
using System.Security.Claims;

namespace revanelxan.Controllers
{
    [Authorize]
    public class SaleController : Controller
    {
        private readonly MyDbContext _context;
        public SaleController(MyDbContext context)
        {
            _context = context; 
        }

        public IActionResult Sale()
        {

            SaleVm vm = new SaleVm()
            {
                Categories = _context.Categories.ToList(),
                Picture = _context.Pictures.FirstOrDefault(x => x.Id == 4)
            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult Sale(Sale sl)
        {
            var updateitem = _context.Products.FirstOrDefault(x => x.Id == sl.Name && x.Status);
            if (updateitem == null) return NotFound();
            updateitem.Count -= sl.Count;
            _context.Products.Update(updateitem);
            _context.Sales.Add(sl);
            _context.SaveChanges();
            return RedirectToAction("salelist", "sale");

        }
        public IActionResult SaleList()
        {
            ViewBag.rol = User.FindFirst(ClaimTypes.Role)?.Value;
            var list = from sl in _context.Sales
                       join ct in _context.Categories on sl.CategoryID equals ct.Id
                       join pr in _context.Products on ct.Id equals pr.CategoryID
                       select new SaleDto
                       {
                           Id = sl.Id,
                           Name = pr.Name,
                           Category = ct.Name,
                           Barcode = pr.Barcode,
                          
                           Cost = pr.Cost,
                           Count = sl.Count,
                           SaleDate = DateTime.Today,
                           TotalCount = sl.TotalCount
                       };
            SaleListVm vm = new SaleListVm()
            {
                products = list
             };
             return View(vm);
            
        }
        public IActionResult saledelete(int? id)
        {
            if (id == null) return NotFound();
            var deleteitem = _context.Sales.FirstOrDefault(x => x.Id == id);

            if (deleteitem == null) return NotFound();
            var adddata = _context.Products.FirstOrDefault(x => x.Id == deleteitem.Name);
            adddata.Count += deleteitem.Count;

            _context.Sales.Remove(deleteitem);
            _context.SaveChanges();
            return RedirectToAction("salelist");
        }

        public IActionResult GetSelectedProduct(int? categoryid)
        {


            var Products = _context.Products.Where(x => x.CategoryID == categoryid && x.Status).ToList();
           

           

            return Json(Products);
        }
        public IActionResult GetSelectedProductCost(int? id)
        {
            var data = _context.Products.Where(x => x.Id == id && x.Status).FirstOrDefault() ;
              return Json(data);
        }
        public IActionResult GetSelectedProductPicture(int? id)
        {
            var picture = _context.Pictures.Where(x => x.CategoryId == id).FirstOrDefault();
            return Json(picture);
        }
    }
}
