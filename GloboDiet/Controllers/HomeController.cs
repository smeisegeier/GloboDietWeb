using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelperLibrary;
using Microsoft.AspNetCore.Hosting;
using GloboDiet.Models;
using GloboDiet.Data;
using System.ComponentModel;

namespace GloboDiet.Controllers
{
    // TODO shift
    public enum ProcessMilestone
    {
        [Description("Interview started")]
        _1_INTERVIEW = 1,

        [Description("Respondent created")]
        _2_RESPONDENT = 2,

        [Description("Meals created")]
        _3_MEALS = 3
    }

    public class HomeController : ControllerBase
    {

        public HomeController(IWebHostEnvironment webHostEnvironment, IRepository repo)
        {
            _webHostEnvironment = webHostEnvironment;
            _repo = repo;

            HttpContext.Session.SetString("SessionUser","itsme");
        }

        #region Private Area
        #endregion

        #region Respondent
        [HttpGet]
        public IActionResult CreateRespondent()
        {
            return View(new Respondent());
        }

        public IActionResult ListRespondents()
        {
            var list = _repo.GetAllRespondents();
            return View(list);
        }
        #endregion

        #region Interview
        [HttpGet]
        public IActionResult CreateInterview()
        {
            var modelNewOrEmpty = Repository.CachedInterview ?? new Interview();
            Repository.CachedInterview = null;

            // ViewModel now also needs the whole List from Process-Enum plus the actual Milestone
            return View(new ViewModels.InterviewCreateEdit(modelNewOrEmpty, _repo.GetAllLocations(), EnumHelper.GetListWithDescription<ProcessMilestone>(), ProcessMilestone._2_RESPONDENT));
        }

        [HttpPost]
        public IActionResult CreateInterview(Interview interview)
        {
            _repo.AddInterview(interview);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult CreateLocationFromInterview(Interview interview)
        {
            // cache object, then redirect
            Repository.CachedInterview = interview;
            return RedirectToAction(nameof(CreateLocation), new { ReturningAction = nameof(CreateInterview) });
        }


        public IActionResult ListInterviews()
        {
            var list = _repo.GetAllInterviews();
            return View(list);
        }
        #endregion
        
        #region Interviewer
        [HttpGet]
        public IActionResult CreateInterviewer()
        {
            return View(new Interviewer());
        }

        [HttpPost]
        public IActionResult CreateInterviewer(Interviewer interviewer)
        {

            _repo.AddInterviewer(interviewer);
            return Redirect("~/Home/Index");
        }

        public IActionResult ListInterviewers()
        {
            var list = _repo.GetAllInterviewers();
            return View(list);
        }
        #endregion
        
        #region Location
        [HttpGet]
        public IActionResult CreateLocation(string returningAction=null)
        {
            return View(new ViewModels.LocationCreateEdit(new Location(), returningAction));
        }

        [HttpPost]
        public IActionResult CreateLocation(Location location, string ReturningAction)
        {
            _repo.AddLocation(location);
            // get Referer
            //return Redirect(Request.Headers["Referer"].ToString());
            return RedirectToAction(ReturningAction);
        }

        public IActionResult ListLocations()
        {
            var list = _repo.GetAllLocations();
            return View(list);
        }

        #endregion

        #region Artefacts
        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        #endregion

    }
}
