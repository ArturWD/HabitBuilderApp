using HabitBuilder.Core.Models;
using HabitBuilder.Core.Models.Extensions;
using HabitBuilder.DAL;
using HabitBuilderApp.Models.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using HabitBuilder.Services;

namespace HabitBuilderApp.Controllers
{
    public class TodayViewController : Controller
    {

        DataContext db = new DataContext();

        // GET: TodayView
        public ActionResult Index()
        {
            var dm = new DayManager(db);
            int userId = Convert.ToInt32(User.Identity.GetUserId());
            UserProfile user = db.UserProfiles.First(u => u.UserProfileId == userId);
            dm.SetStatusesAll(user);
            //db.SaveChanges();

            UserProfile user2 = db.UserProfiles.First(u => u.UserProfileId == userId);
            var dm2 = new DayManager(db);
            var habits = dm2.GetHabits(user2);
            ViewBag.Categories = user2.Habits.Where(h=> h.Category.CategoryName != "").Select(h => h.Category).Distinct();
            return View(habits);
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Create(string HabitName, string HabitDescription, string[] Reasons, int[] Schedule, string HabitCategory)
        {
            int id = Int32.Parse(User.Identity.GetUserId());
            UserProfile user = db.UserProfiles.First(u => u.UserProfileId == id);
            Habit habit = new Habit(true);
            habit.HabitName = HabitName;
            habit.Description = HabitDescription;
            habit.Reasons = Reasons.GetReasons();
            if (HabitCategory != "" && HabitCategory != null)
            {
                string formattedCategory = HabitCategory.Trim(' ').ToLower().Replace("  ", " ");
                if(!user.Categories.Select(c=> c.CategoryName).Contains(formattedCategory))
                {
                    Category newCategory = new Category();
                    newCategory.CategoryName = formattedCategory;
                    user.Categories.Add(newCategory);
                    db.SaveChanges();
                }
                habit.Category = db.UserProfiles.First(u => u.UserProfileId == id).Categories.First(c => c.CategoryName == formattedCategory);
            }
            
            
            List<Day> days = new List<Day>();
            List<Day> dbDays = db.Days.ToList();
            if(Schedule != null)
            {
                foreach (int day in Schedule)
                {
                    Day dayObj = dbDays.First(d => d.DayNumber == day);
                    days.Add(dayObj);
                }
            }
            else
            {
                Day dayObj = dbDays.First(d => d.DayNumber == 1);
                days.Add(dayObj);
            }
            

            habit.Days = days;

            
            DayStatus status = new DayStatus();
            status.StatusDate = DateTime.Now;
            status.Status = db.Statuses.First(s => s.StatusName == "unmarked");
          
            habit.DayStatuses.Add(status);
            user.Habits.Add(habit);
            db.SaveChanges();
            

            return RedirectToAction("Index", "TodayView");
        }



        public ActionResult Delete(int id)
        {
            int userId = Convert.ToInt32(User.Identity.GetUserId());
            UserProfile user = db.UserProfiles.First(u => u.UserProfileId == userId);

            var habitToRemove = db.Habits.First(h => h.HabitId == id);

            if (user.Habits.Where(c => c.Category.CategoryName == habitToRemove.Category.CategoryName).Count() == 1)
            {
                user.Habits.Remove(habitToRemove);
                user.Categories.Remove(user.Categories.First(c => c.CategoryName == habitToRemove.Category.CategoryName));
            }
            else
            {
                user.Habits.Remove(habitToRemove);
            }
            

            db.SaveChanges();

            return RedirectToAction("Index", "TodayView");
        }
    }
}
