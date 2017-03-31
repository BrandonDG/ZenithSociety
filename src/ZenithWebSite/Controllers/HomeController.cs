using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZenithWebSite.Data;
using Microsoft.EntityFrameworkCore;

namespace ZenithWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<ActionResult> Index()
        {
            DateTime lastSunday = getLastSundayFromNow();
            DateTime nextMonday = lastSunday.AddDays(8);

            string thisWeek = lastSunday.AddDays(1).ToString("ddd, MMM dd, yyyy") + " - " +
                nextMonday.AddDays(-1).ToString("ddd, MMM dd, yyyy");
            ViewBag.ThisWeek = thisWeek;

            var events = from ea in db.Events.Include(db => db.Activity)
                         where (ea.IsActive == true || User.Identity.IsAuthenticated)
                            && (ea.FromTime > lastSunday)
                            && (ea.ToTime < nextMonday)
                         orderby ea.FromTime
                         select ea;

            return View(await events.ToListAsync());
        }

        private DateTime getLastSundayFromNow()
        {
            DateTime curDt = DateTime.Now;
            int delta = DayOfWeek.Sunday - curDt.DayOfWeek;
            if (delta == 0)
                delta = -7;
            return curDt.AddDays(delta);
        }
    }
}
