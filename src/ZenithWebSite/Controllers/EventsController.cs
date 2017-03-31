using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ZenithWebSite.Data;
using ZenithWebSite.Models;
using Microsoft.AspNetCore.Authorization;

namespace ZenithWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var events = _context.Events.Include(r => r.Activity);
            return View(await events.ToListAsync());
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            // Written by Iksoo Choi
            //ViewData["ActivityId"] = new SelectList(_context.Activities, "ActivityId", "ActivityId");
            ViewData["ActivityId"] = new SelectList(_context.Activities, "ActivityId", "ActivityDescription");
            //
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,ActivityId,CreationDate,EnteredBy,FromTime,IsActive,ToTime")] Event @event)
        {
            if (ModelState.IsValid)
            {
                @event.CreationDate = DateTime.Now;
                @event.EnteredBy = User.Identity.Name;
                _context.Events.Add(@event);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewData["ActivityId"] = new SelectList(_context.Activities, "ActivityId", "ActivityDescription", @event.ActivityId);

            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.SingleOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            ViewData["ActivityId"] = new SelectList(_context.Activities, "ActivityId", "ActivityDescription", @event.ActivityId);

            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,ActivityId,CreationDate,EnteredBy,FromTime,IsActive,ToTime")] Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(@event).State = EntityState.Modified;
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ActivityId"] = new SelectList(_context.Activities, "ActivityId", "ActivityDescription", @event.ActivityId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var @event = await _context.Events.Include(r => r.Activity).SingleOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.SingleOrDefaultAsync(m => m.EventId == id);
            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
