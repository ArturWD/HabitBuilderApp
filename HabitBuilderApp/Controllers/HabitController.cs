using HabitBuilder.Core.Models;
using HabitBuilder.Core.ViewModels;
using HabitBuilder.DAL;
using HabitBuilder.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HabitBuilderApp.Controllers
{
    public class HabitController : Controller
    {
        DataContext db = new DataContext();

        // GET: Habit
        public ActionResult Index(int habitId)
        {
            Habit dbHabit = db.Habits.First(h => h.HabitId == habitId);
            var habitModel = new HabitViewModel(dbHabit);
            return View(habitModel);
        }

        public ActionResult ChangeStatus(string statusName, int dayId)
        {
            DayManager dm = new DayManager(db);
            dm.ChangeStatus(statusName, dayId);
            Habit habit = db.Habits.First(h => h.DayStatuses.Select(s => s.DayStatusId).Contains(dayId));
            int chainLength = dm.CountChainLength(habit);
            return Json(chainLength.ToString(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult RefreshChainLength(int id)
        {
            DayManager dm = new DayManager(db);
            Habit habit = db.Habits.First(h => h.DayStatuses.Select(s => s.DayStatusId).Contains(id));
            int chainLength = dm.CountChainLength(habit);
            return Json(chainLength.ToString(), JsonRequestBehavior.AllowGet);
        }
    }
}