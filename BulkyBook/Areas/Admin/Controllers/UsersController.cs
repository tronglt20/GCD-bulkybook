using BulkyBook.DataAccess.Data;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var userList = _context.ApplicationUsers.ToList();
            var userRole = _context.UserRoles.ToList();
            var roles = _context.Roles.ToList();

            foreach(var user in userList)
            {
                var roleID = userRole.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(u => u.Id == roleID).Name;
            }

            return View(userList);
        }
    }
}
