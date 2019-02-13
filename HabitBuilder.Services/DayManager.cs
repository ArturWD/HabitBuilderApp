﻿using HabitBuilder.Core.Models;
using HabitBuilder.Core.ViewModels;
using HabitBuilder.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitBuilder.Services
{
    public class DayManager
    {
        DataContext db = new DataContext();

        public List<HabitCardViewModel> GetHabits(UserProfile user)
        {
            var habits = new List<HabitCardViewModel>();
            var dbHabits = user.Habits;
            foreach (var habit in dbHabits)
            {
                var habitView = new HabitCardViewModel();
                habitView.HabitId = habit.HabitId;
                habitView.HabitName = habit.HabitName;
                habitView.ChainLength = CountChainLength(habit);
                habitView.Progress =  CountChainLength(habit)/21;
                habitView.Week = GetLastWeek(habit);

                habits.Add(habitView);
            }

            return habits;
        }

        public void SetStatusesAll(UserProfile user)
        {
            var habits = user.Habits;

            foreach (var habit in habits)
            {
                SetStatusesIndividual(habit);
            }

            db.SaveChanges();
        }

        private void SetStatusesIndividual(Habit habit)
        {
            List<DayStatus> statuses = habit.DayStatuses.OrderBy(s => s.StatusDate).ToList();
            if(statuses.Last().StatusDate.Date != DateTime.Now.Date)
            {
                DateTime lastRecord = statuses.Last().StatusDate;

                while(lastRecord.Date != DateTime.Now.Date)
                {
                    lastRecord.AddDays(1);
                    DayStatus ds = new DayStatus();
                    ds.Status = db.Statuses.First(s => s.StatusName == "fail");
                    ds.StatusDate = lastRecord;

                    habit.DayStatuses.Add(ds);
              
                }
            }

        }

        private int CountChainLength(Habit habit)
        {
            List<DayStatus> statuses = habit.DayStatuses.OrderBy(d => d.StatusDate).Reverse().ToList();
            int chainLength = 0;
            int index = 0;
            while ( (statuses[index].Status.StatusName == "success" || statuses[index].Status.StatusName == "skip" || statuses[index].Status.StatusName == "unmarked") && index < statuses.Count-1)
            {
                if (statuses[index].Status.StatusName == "success") chainLength++;
                index++;
            }
            return chainLength;
        }
        
        private List<DayViewModel> GetLastWeek(Habit habit)
        {
            var days = new List<DayViewModel>();
            var lastweek = habit.DayStatuses.Where(d => d.StatusDate.Date >= DateTime.Now.Date.AddDays(-7)).OrderByDescending(d => d.StatusDate);

            foreach (var date in lastweek)
            {
                DayViewModel day = new DayViewModel();
                day.DayStatus = date.Status.StatusName;
                day.DayId = date.DayStatusId;
                day.Date = date.StatusDate;
                day.HasNote = date.NoteHeadline == "" && date.Note == "";

                days.Add(day);
            }

            if (days.Count < 7)
            {
                int disabled = 7 - days.Count;
                for (int i = 0; i <disabled ; i++)
                {
                    DayViewModel day = new DayViewModel();
                    day.DayStatus = "disabled";

                    days.Add(day);
                }
            }

            days.Reverse();

            return days;


        }
        
    }
}
