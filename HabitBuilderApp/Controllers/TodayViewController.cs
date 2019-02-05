using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HabitBuilderApp.Controllers
{
    public class TodayViewController : Controller
    {
        // GET: TodayView
        public ActionResult Index()
        {
            return View();
        }

        // GET: TodayView/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TodayView/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodayView/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
