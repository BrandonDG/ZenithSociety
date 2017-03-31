using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZenithWebSite.Data;

namespace ZenithWebSite.Models
{
    public class DummyData
    {
        public static void Initialize(ApplicationDbContext db)
        {
            SeedUsers(db);
              
            if (!db.Activities.Any())
            {
                foreach (Activity a in getActivities())
                {
                    db.Activities.Add(a);
                }
                db.SaveChanges();
            }  
            
            if (!db.Events.Any())
            {
                foreach (Event e in getEvents(db))
                {
                    db.Events.Add(e);
                }
                db.SaveChanges();
            }
        }

        private static void SeedUsers(ApplicationDbContext db)
        {
            var admin = new ApplicationUser
            {
                UserName = "a",
                FirstName = "Site",
                LastName = "Admin",
                Email = "a@a.a",
                EmailConfirmed = true,
                NormalizedEmail = "A@A.A",
                NormalizedUserName = "A",
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var member = new ApplicationUser
            {
                UserName = "m",
                FirstName = "Society",
                LastName = "Member",
                Email = "m@m.m",
                EmailConfirmed = true,
                NormalizedEmail = "M@M.M",
                NormalizedUserName = "M",
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var roleStore = new RoleStore<IdentityRole>(db);

            if (!db.Roles.Any(r => r.Name.Equals("admin")))
            {
                //await roleStore.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" });
                var x = roleStore.CreateAsync(new IdentityRole { Name = "Admin", NormalizedName = "admin" }).Result;
            }

            if (!db.Roles.Any(r => r.Name.Equals("member")))
            {
                //await roleStore.CreateAsync(new IdentityRole { Name = "Member", NormalizedName = "MEMBER" });
                var x = roleStore.CreateAsync(new IdentityRole { Name = "Member", NormalizedName = "member" }).Result;
            }

            if (!db.Users.Any(u => u.Email == "a@a.a"))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(admin, "P@$$w0rd");
                admin.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(db);
                var x = userStore.CreateAsync(admin).Result;
                userStore.AddToRoleAsync(admin, "admin").Wait();
                userStore.AddToRoleAsync(admin, "member").Wait();
            }

            if (!db.Users.Any(u => u.Email == "m@m.m"))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(member, "P@$$w0rd");
                member.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(db);
                var x = userStore.CreateAsync(member).Result;
                userStore.AddToRoleAsync(member, "member").Wait();
            }
            var res = db.SaveChangesAsync().Result;
        }

        private static Activity[] getActivities()
        {

            List<Activity> activities = new List<Activity>()
            {
                new Activity()
                {
                    ActivityDescription = "Senior's Golf Tournament",
                    CreationDate = new DateTime(2017, 02, 01)
                },
                new Activity()
                {
                    ActivityDescription = "Youth Golf Tournament",
                    CreationDate = new DateTime(2017, 02, 01)
                },
                new Activity()
                {
                    ActivityDescription = "Junior Golf Tournament",
                    CreationDate = new DateTime(2017, 02, 01)
                },
                new Activity()
                {
                    ActivityDescription = "Youth's Hockey Tournament",
                    CreationDate = new DateTime(2017, 02, 02)
                },
                new Activity()
                {
                    ActivityDescription = "Junior's Hockey Tournament",
                    CreationDate = new DateTime(2017, 02, 02)
                },
                new Activity()
                {
                    ActivityDescription = "Senior's Ballroom Dance",
                    CreationDate = new DateTime(2017, 02, 03)
                },
                new Activity()
                {
                    ActivityDescription = "Lady's Hotdog Eating Contest",
                    CreationDate = new DateTime(2017, 02, 04)
                },
                new Activity()
                {
                    ActivityDescription = "Senior's Hotdog Eating Contest",
                    CreationDate = new DateTime(2017, 02, 04)
                },
                new Activity()
                {
                    ActivityDescription = "Swimming Lessons for the youth",
                    CreationDate = new DateTime(2017, 02, 05)
                },
                new Activity()
                {
                    ActivityDescription = "Leadership General Assembly Meeting I",
                    CreationDate = new DateTime(2017, 02, 05)
                },
                new Activity()
                {
                    ActivityDescription = "Leadership General Assembly Meeting II",
                    CreationDate = new DateTime(2017, 02, 05)
                },
                new Activity()
                {
                    ActivityDescription = "Youth Bowling Tournament",
                    CreationDate = new DateTime(2017, 02, 06)
                },
                new Activity()
                {
                    ActivityDescription = "Youth craft lessons I",
                    CreationDate = new DateTime(2017, 02, 06)
                },
                new Activity()
                {
                    ActivityDescription = "Youth craft lessons II",
                    CreationDate = new DateTime(2017, 02, 07)
                },
                new Activity()
                {
                    ActivityDescription = "Youth choir practice",
                    CreationDate = new DateTime(2017, 02, 07)
                },
                new Activity()
                {
                    ActivityDescription = "Lunch",
                    CreationDate = new DateTime(2017, 02, 07)
                },
                new Activity()
                {
                    ActivityDescription = "Bingo Tournament",
                    CreationDate = new DateTime(2017, 02, 07)
                },
                new Activity()
                {
                    ActivityDescription = "BBQ Lunch",
                    CreationDate = new DateTime(2017, 02, 07)
                },
                new Activity()
                {
                    ActivityDescription = "Garage Sale",
                    CreationDate = new DateTime(2017, 02, 07)
                },
                new Activity()
                {
                    ActivityDescription = "Pancake Breakfast",
                    CreationDate = new DateTime(2017, 02, 07)
                },
                new Activity()
                {
                    ActivityDescription = "Young ladies cooking lessons",
                    CreationDate = new DateTime(2017, 02, 08)
                }
            };
            return activities.ToArray();
        }

        private static Event[] getEvents(ApplicationDbContext context)
        {

            List<Event> events = new List<Event>()
            {
                new Event()
                {
                    FromTime = new DateTime(2017, 03, 21, 08, 30, 0),
                    ToTime = new DateTime(2017, 03, 21, 10, 30, 0),
                    EnteredBy = "a",
                    CreationDate = new DateTime(2017, 02, 04),
                    IsActive = false,
                    ActivityId = context.Activities
                        .FirstOrDefault(a => a.ActivityDescription == "Senior's Golf Tournament")
                        .ActivityId
                },
                new Event()
                {
                    FromTime = new DateTime(2017, 03, 21, 09, 30, 0),
                    ToTime = new DateTime(2017, 03, 21, 11, 30, 0),
                    EnteredBy = "a",
                    CreationDate = new DateTime(2017, 02, 04),
                    IsActive = false,
                    ActivityId = context.Activities
                        .FirstOrDefault(a => a.ActivityDescription == "Young ladies cooking lessons")
                        .ActivityId
                },
                new Event()
                {
                    FromTime = new DateTime(2017, 03, 22, 08, 30, 0),
                    ToTime = new DateTime(2017, 03, 22, 10, 30, 0),
                    EnteredBy = "a",
                    CreationDate = new DateTime(2017, 02, 04),
                    IsActive = false,
                    ActivityId = context.Activities
                        .FirstOrDefault(a => a.ActivityDescription == "Leadership General Assembly Meeting I")
                        .ActivityId
                },
                new Event()
                {
                    FromTime = new DateTime(2017, 03, 23, 08, 30, 0),
                    ToTime = new DateTime(2017, 03, 23, 09, 30, 0),
                    EnteredBy = "a",
                    CreationDate = new DateTime(2017, 02, 04),
                    IsActive = false,
                    ActivityId = context.Activities
                        .FirstOrDefault(a => a.ActivityDescription == "Youth craft lessons I")
                        .ActivityId
                },
                new Event()
                {
                    FromTime = new DateTime(2017, 03, 23, 08, 30, 0),
                    ToTime = new DateTime(2017, 03, 23, 09, 30, 0),
                    EnteredBy = "a",
                    CreationDate = new DateTime(2017, 02, 04),
                    IsActive = false,
                    ActivityId = context.Activities
                        .FirstOrDefault(a => a.ActivityDescription == "Swimming Lessons for the youth")
                        .ActivityId
                },
                new Event()
                {
                    FromTime = new DateTime(2017, 03, 23, 10, 30, 0),
                    ToTime = new DateTime(2017, 03, 23, 12, 30, 0),
                    EnteredBy = "a",
                    CreationDate = new DateTime(2017, 02, 04),
                    IsActive = false,
                    ActivityId = context.Activities
                        .FirstOrDefault(a => a.ActivityDescription == "Lady's Hotdog Eating Contest")
                        .ActivityId
                },
                new Event()
                {
                    FromTime = new DateTime(2017, 03, 24, 09, 30, 0),
                    ToTime = new DateTime(2017, 03, 24, 11, 30, 0),
                    EnteredBy = "a",
                    CreationDate = new DateTime(2017, 02, 04),
                    IsActive = false,
                    ActivityId = context.Activities
                        .FirstOrDefault(a => a.ActivityDescription == "Youth choir practice")
                        .ActivityId
                },
                new Event()
                {
                    FromTime = new DateTime(2017, 03, 24, 10, 30, 0),
                    ToTime = new DateTime(2017, 03, 24, 12, 30, 0),
                    EnteredBy = "a",
                    CreationDate = new DateTime(2017, 02, 04),
                    IsActive = false,
                    ActivityId = context.Activities
                        .FirstOrDefault(a => a.ActivityDescription == "Bingo Tournament")
                        .ActivityId
                },
                new Event()
                {
                    FromTime = new DateTime(2017, 03, 24, 12, 00, 0),
                    ToTime = new DateTime(2017, 03, 24, 01, 30, 0),
                    EnteredBy = "a",
                    CreationDate = new DateTime(2017, 02, 04),
                    IsActive = false,
                    ActivityId = context.Activities
                        .FirstOrDefault(a => a.ActivityDescription == "BBQ Lunch")
                        .ActivityId
                },
                new Event()
                {
                    FromTime = new DateTime(2017, 03, 25, 07, 30, 0),
                    ToTime = new DateTime(2017, 03, 25, 08, 30, 0),
                    EnteredBy = "a",
                    CreationDate = new DateTime(2017, 02, 04),
                    IsActive = false,
                    ActivityId = context.Activities
                        .FirstOrDefault(a => a.ActivityDescription == "Pancake Breakfast")
                        .ActivityId
                },
                new Event()
                {
                    FromTime = new DateTime(2017, 03, 25, 08, 30, 0),
                    ToTime = new DateTime(2017, 03, 25, 10, 30, 0),
                    EnteredBy = "a",
                    CreationDate = new DateTime(2017, 02, 04),
                    IsActive = false,
                    ActivityId = context.Activities
                        .FirstOrDefault(a => a.ActivityDescription == "Junior's Hockey Tournament")
                        .ActivityId
                },
                new Event()
                {
                    FromTime = new DateTime(2017, 03, 25, 10, 30, 0),
                    ToTime = new DateTime(2017, 03, 25, 12, 30, 0),
                    EnteredBy = "a",
                    CreationDate = new DateTime(2017, 02, 04),
                    IsActive = false,
                    ActivityId = context.Activities
                        .FirstOrDefault(a => a.ActivityDescription == "Junior Golf Tournament")
                        .ActivityId
                },
                new Event()
                {
                    FromTime = new DateTime(2017, 03, 25, 12, 30, 0),
                    ToTime = new DateTime(2017, 03, 25, 01, 30, 0),
                    EnteredBy = "a",
                    CreationDate = new DateTime(2017, 02, 04),
                    IsActive = false,
                    ActivityId = context.Activities
                        .FirstOrDefault(a => a.ActivityDescription == "Lunch")
                        .ActivityId
                },
                new Event()
                {
                    FromTime = new DateTime(2017, 03, 26, 11, 30, 0),
                    ToTime = new DateTime(2017, 03, 26, 15, 30, 0),
                    EnteredBy = "a",
                    CreationDate = new DateTime(2017, 02, 04),
                    IsActive = false,
                    ActivityId = context.Activities
                        .FirstOrDefault(a => a.ActivityDescription == "Garage Sale")
                        .ActivityId
                }
            };
            return events.ToArray();
        }
    }
}
