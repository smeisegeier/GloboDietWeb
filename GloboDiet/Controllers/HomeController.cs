using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelperLibrary;
using Microsoft.AspNetCore.Hosting;
using GloboDiet.Models;
using GloboDiet.Services;
using System.ComponentModel;
using GloboDiet.ViewModels;
using Microsoft.EntityFrameworkCore;
using GloboDiet;

namespace GloboDiet.Controllers
{

    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        //private readonly IRepository _repo;
        private readonly HttpContext _httpContext;

        // example for domain repo
        private readonly IRepositoryNew<Interview> _repoInterview;
        private readonly IRepositoryNew<Interviewer> _repoInterviewer;
        private readonly IRepositoryNew<Location> _repoLocation;
        private readonly IRepositoryNew<Respondent> _repoRespondent;

        public HomeController(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, IRepositoryNew<Interview> repoInterview, IRepositoryNew<Interviewer> repoInterviewer, IRepositoryNew<Location> repoLocation, IRepositoryNew<Respondent> repoRespondent)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContext = httpContextAccessor.HttpContext;
            _repoInterview = repoInterview;
            _repoInterviewer = repoInterviewer;
            _repoLocation = repoLocation;
            _repoRespondent = repoRespondent;

            seedAll();
        }

        public IActionResult Index()
        {
            // testing session mechanics
            _httpContext.Session.SetString("SessionUser", "itsme");
            return View(new ViewModelBase(getNewNavigationBar()));
        }


        #region Private Area
        private NavigationBar getNewNavigationBar() => new NavigationBar(_repoInterview.GetItemsCount(), _repoInterviewer.GetItemsCount(), _repoInterview.GetSqlConnectionType());
        #endregion

        private void seedAll()
        {
            _repoInterviewer.SeedItems(Interviewer.GetSeededValues());
            _repoLocation.SeedItems(Location.GetSeededValues());
            _repoRespondent.SeedItems(Respondent.GetSeededValues());
        }

        #region Respondent
        [HttpGet]
        public IActionResult CreateRespondent()
        {
            return View(new Respondent());
        }

        public IActionResult ListRespondents()
        {
            var list = _repoRespondent.GetAllItems();
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
            return View(new InterviewCreateEdit(modelNewOrEmpty, _repoLocation.GetAllItems(), EnumHelper.GetListWithDescription<ProcessMilestone>(), ProcessMilestone._2_RESPONDENT, getNewNavigationBar()));
        }

        [HttpPost]
        public IActionResult CreateInterview(Interview interview)
        {
            _repoInterview.AddItem(interview);
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
            var list = _repoInterview.GetAllItems();
            return View(new InterviewsList(list, getNewNavigationBar()));
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

            _repoInterviewer.AddItem(interviewer);
            return Redirect("~/Home/Index");
        }

        public IActionResult ListInterviewers()
        {
            var list = _repoInterviewer.GetAllItems();
            return View(list);
        }
        #endregion
        
        #region Location
        [HttpGet]
        public IActionResult CreateLocation(string returningAction=null)
        {
            return View(new ViewModels.LocationCreateEdit(new Location(), getNewNavigationBar(), returningAction));
        }

        [HttpPost]
        public IActionResult CreateLocation(Location location, string ReturningAction)
        {
            _repoLocation.AddItem(location);
            // get Referer
            //return Redirect(Request.Headers["Referer"].ToString());
            return RedirectToAction(ReturningAction);
        }

        public IActionResult ListLocations()
        {
            var list = _repoLocation.GetAllItems();
            return View(new LocationsList(list, getNewNavigationBar()));
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
