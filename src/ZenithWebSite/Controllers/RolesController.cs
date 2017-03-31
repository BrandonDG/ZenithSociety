using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZenithWebSite.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ZenithWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Index.
        public IActionResult Index()
        {
            List<IdentityRole> roles = _context.Roles.ToList();
            return View(roles);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] IdentityRole @role)
        {
            if (ModelState.IsValid)
            {
                var roleStore = new RoleStore<IdentityRole>(_context);
                await roleStore.CreateAsync(new IdentityRole { Name = @role.Name, NormalizedName = @role.Name.ToUpper() });
                return RedirectToAction("Index");
            }
            return View(@role);
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //.Include(r => r.Activity)
            //var @event = await _context.Events.SingleOrDefaultAsync(m => m.EventId == id);
            var roleStore = new RoleStore<IdentityRole>(_context);
            IdentityRole role = await roleStore.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var roleStore = new RoleStore<IdentityRole>(_context);
            IdentityRole role = await roleStore.FindByIdAsync(id);
            await roleStore.DeleteAsync(role);
            return RedirectToAction("Index");
        }
    }
}