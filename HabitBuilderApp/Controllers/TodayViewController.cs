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

namespace HabitBuilderApp.Controllers
{
    public class TodayViewController : Controller
    {

        DataContext db = new DataContext();

        // GET: TodayView
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create(string HabitName, string HabitDescription, string[] Reasons, int[] Schedule)
        {
            int i = Int32.Parse(User.Identity.GetUserId());
            Habit habit = new Habit();
            habit.HabitName = HabitName;
            habit.Description = HabitDescription;
            habit.Reasons = Reasons.GetReasons();
            habit.Category = new Category{CategoryName = "Без категории" };

            List<Day> days = new List<Day>();
            foreach (var day in Schedule)
            {
                Day dayObj = db
                days.Add(reasonObj);
            }

            habit.Days = Schedule.GetDays();
            var i = 1;
            return View();
        }


  

        // GET: TodayView/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TodayView/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TodayView/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TodayView/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
