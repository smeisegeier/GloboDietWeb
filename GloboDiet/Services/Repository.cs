﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GloboDiet.Models;
using HelperLibrary;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace GloboDiet.Data
{
    public interface IRepository
    {
        /* Interview */
        Interview GetInterviewById(int id);
        List<Interview> GetAllInterviews();
        bool AddInterview(Interview interview);

        /* Interviewer */
        Interviewer GetInterviewerById(int id);
        List<Interviewer> GetAllInterviewers();
        bool AddInterviewer(Interviewer interviewer);

        /* Respondent */
        Respondent GetRespondentById(int id);
        List<Respondent> GetAllRespondents();
        bool AddRespondent(Respondent respondent);

        /*Masterdata*/
        Location GetLocationById(int id);
        List<Location> GetAllLocations();
        bool AddLocation(Location location);

    }
    public class Repository : IRepository
    {
        #region StaticArea
        public static Interview CachedInterview { get; set; } = null;
        public static EfCoreHelper.SqlConnectionType CurrentSqlConnectionType { get; set; }
        #endregion

        private readonly GloboDietDbContext _context;

        // HACK
        public static int PillCountInterviews { get; set; }
        public static int PillCountInterviewers { get; set; }

        public Repository(IWebHostEnvironment env, GloboDietDbContext context)
        {
            _context = context;
            CurrentSqlConnectionType = EfCoreHelper.GetSqlConnectionType(_context);

            if (env.EnvironmentName != "Production")
                writeDefaultValues();
            setPillBoxes();
        }


        #region Interview
        public Interview GetInterviewById(int id)
        {
            using (var context = new GloboDietDbContext())
            {
                return context.Interviews.FirstOrDefault(x => x.Id == id);
            }
        }
        public List<Interview> GetAllInterviews()
        {
            using (var context = new GloboDietDbContext())
            {
                return context.Interviews.ToList();
            }
        }

        public bool AddInterview(Interview interview)
        {
            using (var context = new GloboDietDbContext())
            {
                context.Interviews.Add(interview);
                context.SaveChanges();
                return true;
            }
        }
        #endregion
        #region Interviewer
        public Interviewer GetInterviewerById(int id)
        {
            using (var context = new GloboDietDbContext())
            {
                return context.Interviewers.FirstOrDefault(x => x.Id == id);
            }
        }
        public List<Interviewer> GetAllInterviewers()
        {
            //return _context.Interviewers.ToList();
            using (var context = new GloboDietDbContext())
            {
                return context.Interviewers.ToList();
            }
        }

        public bool AddInterviewer(Interviewer interviewer)
        {
            using (var context = new GloboDietDbContext())
            {
                context.Interviewers.Add(interviewer);
                context.SaveChanges();
                return true;
            }
        }

        #endregion
        #region Location
        public Location GetLocationById(int id)
        {
            using (var context = new GloboDietDbContext())
            {
                return context.Locations.FirstOrDefault(x => x.Id == id);
            }
        }
        public List<Location> GetAllLocations()
        {
            using (var context = new GloboDietDbContext())
            {
                return context.Locations.ToList();
            }
        }

        public bool AddLocation(Location location)
        {
            using (var context = new GloboDietDbContext())
            {
                context.Locations.Add(location);
                context.SaveChanges();
                return true;
            }
        }
        #endregion
        #region Respondent
        public Respondent GetRespondentById(int id)
        {
            using (var context = new GloboDietDbContext())
            {
                return context.Respondents.FirstOrDefault(x => x.Id == id);
            }
        }
        public List<Respondent> GetAllRespondents()
        {
            using (var context = new GloboDietDbContext())
            {
                return context.Respondents.ToList();
            }
        }

        public bool AddRespondent(Respondent respondent)
        {
            using (var context = new GloboDietDbContext())
            {
                context.Respondents.Add(respondent);
                context.SaveChanges();
                return true;
            }
        }
        #endregion
        #region PrivateSector
        private void writeDefaultValues()
        {
            using (var context = new GloboDietDbContext())
            {
                if (!context.Interviewers.Any())
                {
                    var list = Interviewer.GenerateDefaultValues();
                    context.Interviewers.AddRange(list);
                }
                if (!context.Locations.Any())
                {
                    var list = Location.GenerateDefaultValues();
                    context.Locations.AddRange(list);
                }
                if (!context.PlacesOfMeal.Any())
                {
                    var list = PlaceOfMeal.GenerateDefaultValues();
                    context.PlacesOfMeal.AddRange(list);
                }
                if (!context.Respondents.Any())
                {
                    var list = Respondent.GenerateDefaultValues();
                    context.Respondents.AddRange(list);
                }
                context.SaveChanges();
            }
        }
        private void setPillBoxes()
        {
            PillCountInterviews = _context.Interviews.Count();
            PillCountInterviewers = _context.Interviewers.Count();
        }
        #endregion
    }
}
