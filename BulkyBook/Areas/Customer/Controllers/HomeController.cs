using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
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
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
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
            ShoppingCart cartObj = new ShoppingCart()
            {
                Product = product,
                ProductId = product.Id
            };

            if (product == null)
            {
                return NotFound();
            }

            return View(cartObj);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize]
        public async Task<IActionResult> AddToCart(ShoppingCart CartObject)
        {
            CartObject.Id = 0;

            if (ModelState.IsValid)
            {   
                // add to cart
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                CartObject.ApplicationUserId = claim.Value;

                ShoppingCart cartFromDb = _context.ShoppingCarts.FirstOrDefault(
                    u => u.ApplicationUserId == CartObject.ApplicationUserId && u.ProductId == CartObject.ProductId);
                if(cartFromDb == null)
                {
                    // no record exits in database for that user for thatproduct
                }
            }
            else
            {

                var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.CoverType)
                .FirstOrDefaultAsync(m => m.Id == CartObject.ProductId);
                ShoppingCart cartObj = new ShoppingCart()
                {
                    Product = product,
                    ProductId = product.Id
                };

                if (product == null)
                {
                    return NotFound();
                }
            }

        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
