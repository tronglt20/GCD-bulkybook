using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        /*private readonly ILogger<HomeController> _logger;*/
        private readonly ApplicationDbContext _context;


        public HomeController(ApplicationDbContext context)
        {
            /*_logger = logger;*/
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Category).Include(p => p.CoverType);
            return View(await applicationDbContext.ToListAsync());
        }


        public async Task<IActionResult> Detail(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.CoverType)
                .FirstOrDefaultAsync(m => m.Id == id);

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
           

            ReqAccess reqAccess = _context.ReqAccess.FirstOrDefault(
                u => u.ApplicationUserId == claim.Value && u.ProductId == product.Id);

            if(reqAccess == null)
            {
                reqAccess = new ReqAccess()
                {
                    Product = product,
                    ProductId = product.Id,
                    Status = SD.Status_Block
                };
            }     
            return View(reqAccess);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Detail(ReqAccess Object)
        {
            Object.Id = 0;

            if (ModelState.IsValid)
            {

                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                Object.ApplicationUserId = claim.Value;

                ReqAccess reqFromDb = _context.ReqAccess.FirstOrDefault(
                    u => u.ApplicationUserId == Object.ApplicationUserId && u.ProductId == Object.ProductId);

                if (reqFromDb == null)
                {
                    // no record exits in database for that user for thatproduct
                    Object.Status = SD.Status_InProcess;
                    _context.ReqAccess.Update(Object);
                }
                else
                { // return POP UP message
                }

                _context.SaveChanges();

               // var count = _context.ShoppingCarts
               //     .ToArrayAsync(u => u.ApplicationUserId == CartObject.ApplicationUserId)
               //     .ToList().Count();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.CoverType)
                .FirstOrDefaultAsync(m => m.Id == Object.ProductId);
                ReqAccess obj = new ReqAccess()
                {
                    Product = product,
                    ProductId = product.Id
                };
                return View(obj);
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
