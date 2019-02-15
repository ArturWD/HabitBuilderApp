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
            var dm = new DayManager();
            int userId = Convert.ToInt32(User.Identity.GetUserId());
            UserProfile user = db.UserProfiles.First(u => u.UserProfileId == userId);
            dm.SetStatusesAll(user);
            db.SaveChanges();

            user = db.UserProfiles.First(u => u.UserProfileId == userId);
            dm = new DayManager();
            var habits = dm.GetHabits(user);
            return View(habits);
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult Create(string HabitName, string HabitDescription, string[] Reasons, int[] Schedule)
        {
            int id = Int32.Parse(User.Identity.GetUserId());
            Habit habit = new Habit();
            habit.HabitName = HabitName;
            habit.Description = HabitDescription;
            habit.Reasons = Reasons.GetReasons();
            habit.Category = new Category{CategoryName = "Без категории" };
            
            List<Day> days = new List<Day>();
            List<Day> dbDays = db.Days.ToList();
            foreach (int day in Schedule)
            {
                Day dayObj = dbDays.First(d => d.DayNumber == day);
                days.Add(dayObj);
            }

            habit.Days = days;


            UserProfile user = db.UserProfiles.First(u => u.UserProfileId == id);
            
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

            user.Habits.Remove(habitToRemove);

            db.SaveChanges();

            return RedirectToAction("Index", "TodayView");
        }
    }
}
