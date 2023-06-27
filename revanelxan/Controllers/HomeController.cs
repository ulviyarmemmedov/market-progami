using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using revanelxan.Data;
using revanelxan.Dtos;
using revanelxan.Models;
using revanelxan.VM;
using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace revanelxan.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;

        public HomeController(ILogger<HomeController> logger,MyDbContext context)
        {
            _logger = logger;
            _context = context;

            var data = _context.Products.ToList();
            foreach (var item in data)
            {
                
                if ((item.EndTime - DateTime.Now).Days <= 0)
                {
                    if (item.Status == true || item.Count==0)
                    {
                        //_context.Products.Remove(item);

                        item.Status = false;

                    }
                }
            }
            
            _context.SaveChanges();
        }

        public IActionResult Index()
        {
            IndexVm vm = new IndexVm() 
            { Picture = _context.Pictures.Where(x=>x.Id == 5).FirstOrDefault() };

            return View(vm);
        }
        public IActionResult Stoce()
        {
            var data = from pr in _context.Products
                      join ct in _context.Categories on pr.CategoryID equals ct.Id
                      select new StoceDto
                      {
                          Id = pr.Id,
                          Name = pr.Name,
                          Category = ct.Name,
                          Barcode = pr.Barcode,
                          
                          Cost = pr.Cost,
                          Count = pr.Count,
                          Status=pr.Status,
                          EndTime=pr.EndTime,
                          RestOfDay=(pr.EndTime- DateTime.Now).Days
                                                    
                          
                      };
            StoceVm vm = new StoceVm()
            {
                Products = data
            };
            return View(vm);
        }
        public IActionResult Ern()
        {
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

        [AllowAnonymous]
        public IActionResult login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> login(string name, string pass) {

            if (name == null || pass == null) NotFound();

            var item = _context.Users.Where(c => c.UserName == name && c.Password == pass).FirstOrDefault();
            if (item != null)
            {
                var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name,item.UserName),
                            new Claim(ClaimTypes.Role,item.Role),
                            new Claim("pass",item.Password)
                        };
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity), (authProperties));
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult signout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login");
        }






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}