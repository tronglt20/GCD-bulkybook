using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ReqAccessController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReqAccessController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            IEnumerable<ReqAccess> reqAccesses;

            if (User.IsInRole(SD.Role_Admin))
            {
                reqAccesses = _context.ReqAccess.Include(u => u.ApplicationUser)
                                                .Include(p=>p.Product);
            }
            else
            {
                reqAccesses = _context.ReqAccess.Include(u => u.ApplicationUser)
                                                .Include(p => p.Product)
                                                .Where(u=>u.ApplicationUserId == claim.Value);
            }

            return View(reqAccesses);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Accept(int? id)
        {
            ReqAccess reqAccess = _context.ReqAccess.FirstOrDefault(
             u => u.Id == id);

            reqAccess.Status = SD.Stutus_Accepted;
            _context.ReqAccess.Update(reqAccess);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reject(int? id)
        {
            ReqAccess reqAccess = _context.ReqAccess.FirstOrDefault(
              u => u.Id == id);

            reqAccess.Status = SD.Stutus_Rejected;
            _context.ReqAccess.Update(reqAccess);

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
