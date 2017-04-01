using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZenithWebSite.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using ZenithWebSite.Models;
using NuGet.Packaging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ZenithWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersRolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersRolesController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // Index.
        public IActionResult Index()
        {
            return View(getUsersAndRoles()); 
        }

        // GET: UsersRoles/GrantRole/{id}
        public ActionResult GrantRole(string id)
        {
            ViewData["id"] = id;
            string query = "SELECT * FROM AspNetRoles WHERE Id NOT IN(SELECT RoleId from AspNetUserRoles WHERE UserId = '" + id + "')";

            return View(_context.Roles.FromSql(query).ToList());
        }

        // POST: UsersRoles/GrantRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GrantRole(IFormCollection data)
        {
            try
            {
                var role = _context.Roles.FirstOrDefault(r => r.Id == data["roleId"].ToString());
                var user = _context.Users.FirstOrDefault(u => u.Id == data["userId"].ToString());

                await (new UserStore<ApplicationUser>(_context).AddToRoleAsync(user, role.NormalizedName));
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                string query = "SELECT * FROM AspNetRoles WHERE Id NOT IN(SELECT RoleId from AspNetUserRoles WHERE UserId = '" + data["userId"].ToString() + "')";
                return View(_context.Roles.FromSql(query).ToList());
            }
        }

        // GET: UsersRoles/RevokeRole/{id}
        public ActionResult RevokeRole(string id)
        {
            ViewData["id"] = id;
            return View(_context.Roles.FromSql(getRevokeRoles(id)).ToList());
        }

        // POST: UsersRoles/RevokeRole
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RevokeRole(IFormCollection data)
        {
            try
            {
                var role = _context.Roles.FirstOrDefault(r => r.Id == data["roleId"].ToString());
                var user = _context.Users.FirstOrDefault(u => u.Id == data["userId"].ToString());

                await (new UserStore<ApplicationUser>(_context).RemoveFromRoleAsync(user, role.NormalizedName));
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(_context.Roles.FromSql(getRevokeRoles(data["userId"].ToString())).ToList());
            }
        }

        // Get users and their roles.
        private Dictionary<ApplicationUser, List<IdentityRole>> getUsersAndRoles()
        {
            Dictionary<ApplicationUser, List<IdentityRole>> usersRoles = new Dictionary<ApplicationUser, List<IdentityRole>>();
            var usersQuery = from User in _context.Users
                             join UserRoles in _context.UserRoles on User.Id equals UserRoles.UserId
                             join Roles in _context.Roles on UserRoles.RoleId equals Roles.Id
                             select new { User, Roles };
            var result = usersQuery.ToList();

            foreach (var pair in result)
            {
                if (usersRoles.ContainsKey(pair.User))
                {
                    usersRoles[pair.User].Add(pair.Roles);
                }
                else
                {
                    List<IdentityRole> roles = new List<IdentityRole>();
                    roles.Add(pair.Roles);
                    usersRoles.Add(pair.User, roles);
                }
            }

            return usersRoles;
        }

        // Query, if user is seeded admin, filter out Admin role.
        private string getRevokeRoles(string userId)
        {
            //2d8ef36e-7a9c-4951-beab-2419d26a08e6
            //b1814150-daf7-4557-b347-46fd0d0a15e1
            string query;
            if (userId.Equals("2d8ef36e-7a9c-4951-beab-2419d26a08e6"))
            {
                query = "SELECT * FROM AspNetRoles WHERE Id IN(SELECT RoleId from AspNetUserRoles WHERE UserId = '" + userId + "') "
                    + "AND Name != 'Admin'";
            }
            else
            {
                query = "SELECT * FROM AspNetRoles WHERE Id IN(SELECT RoleId from AspNetUserRoles WHERE UserId = '" + userId + "')";
            }
            return query;
        }
    }
}