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

        public ActionResult GetNote(int dayId)
        {
            var day = db.DayStatuses.First(d => d.DayStatusId == dayId);
            NoteView note = new NoteView {
                DayId = day.DayStatusId,
                Date = day.StatusDate,
                NoteText = day.Note == null ? "" : day.Note,
                Headline = day.NoteHeadline == null ? "Заголовок" : day.NoteHeadline
            };
            string noteJSON = Newtonsoft.Json.JsonConvert.SerializeObject(note);
            return Json(noteJSON, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllNotes(int habitId)
        {
            DayManager dm = new DayManager(db);

            List<NoteView> notes = dm.GetAllNotes(habitId);
            
            string notesJSON = Newtonsoft.Json.JsonConvert.SerializeObject(notes);
            return Json(notesJSON, JsonRequestBehavior.AllowGet);
        }

        public void SaveNote(int dayId, string header, string noteText)
        {
            var day = db.DayStatuses.First(d => d.DayStatusId == dayId);
            day.NoteHeadline = header;
            day.Note = noteText;
            day.Status = day.Status;
            db.SaveChanges();
        }

        public void DeleteNote(int dayId)
        {
            var day = db.DayStatuses.First(d => d.DayStatusId == dayId);
            day.NoteHeadline = "";
            day.Note = "";
            day.Status = day.Status;
            db.SaveChanges();
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