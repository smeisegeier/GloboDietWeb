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
            // if no content needed just pass ViewModelBase
            return View(new ViewModelBase(getNewNavigationBar()));
        }


        #region Private Area
        private NavigationBar getNewNavigationBar() => new NavigationBar(_repoInterview.ItemsGetCount(), _repoInterviewer.ItemsGetCount(), _repoLocation.ItemsGetCount(), _repoRespondent.ItemsGetCount(), _repoInterview.GetSqlConnectionType());

        private void seedAll()
        {
            _repoInterviewer.ItemsSeed(Interviewer.GetSeededValues());
            _repoLocation.ItemsSeed(Location.GetSeededValues());
            _repoRespondent.ItemsSeed(Respondent.GetSeededValues());
            _repoInterview.ItemsSeed(Interview.GetSeededValues());
        }
        #endregion


        #region Respondent
        [HttpGet]
        public IActionResult RespondentCreate()
        {
            return View(new RespondentCreateEdit(new Respondent(), getNewNavigationBar()));
        }

        public IActionResult RespondentsList()
        {
            var list = _repoRespondent.ItemsGetAll();
            return View(new RespondentsList(list, getNewNavigationBar()));
        }
        #endregion

        #region Interview
        [HttpGet]
        public IActionResult InterviewCreate()
        {
            var modelNewOrEmpty = Repository.CachedInterview ?? new Interview();
            Repository.CachedInterview = null;

            // ViewModel now also needs the whole List from Process-Enum plus the actual Milestone
            return View(new InterviewCreateEdit(modelNewOrEmpty, _repoLocation.ItemsGetAll(), EnumHelper.GetListWithDescription<ProcessMilestone>(), ProcessMilestone._2_RESPONDENT, getNewNavigationBar()));
        }

        [HttpPost]
        public IActionResult InterviewCreate(Interview interview)
        {
            _repoInterview.ItemAdd(interview);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult CreateLocationFromInterview(Interview interview)
        {
            // cache object, then redirect
            Repository.CachedInterview = interview;
            return RedirectToAction(nameof(LocationCreate), new { ReturningAction = nameof(InterviewCreate) });
        }

        [HttpGet]
        public IActionResult InterviewEdit(int id)
        {
            var interview = _repoInterview.ItemGetById(id);
            return View(new InterviewCreateEdit(interview, _repoLocation.ItemsGetAll(), EnumHelper.GetListWithDescription<ProcessMilestone>(), ProcessMilestone._3_MEALS, getNewNavigationBar()));
        }

        [HttpPost]
        public IActionResult InterviewEdit(Interview interview)
        {
            _repoInterview.ItemUpdate(interview);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult InterviewsList()
        {
            var list = _repoInterview.ItemsGetAll();
            return View(new InterviewsList(list, getNewNavigationBar()));
        }

        public IActionResult InterviewDetails(int id) => Json(_repoInterview.ItemGetById(id));

        #endregion

        #region Interviewer
        [HttpGet]
        public IActionResult InterviewerCreate()
        {
            return View(new ViewModels.InterviewerCreateEdit( new Interviewer(), getNewNavigationBar()));
        }

        [HttpPost]
        public IActionResult InterviewerCreate(Interviewer interviewer)
        {

            _repoInterviewer.ItemAdd(interviewer);
            return Redirect("~/Home/Index");
        }

        public IActionResult InterviewersList()
        {
            var list = _repoInterviewer.ItemsGetAll();
            return View(new InterviewersList(list, getNewNavigationBar()));
        }
        #endregion
        
        #region Location
        [HttpGet]
        public IActionResult LocationCreate(string returningAction=null)
        {
            return View(new ViewModels.LocationCreateEdit(new Location(), getNewNavigationBar(), returningAction));
        }

        [HttpPost]
        public IActionResult LocationCreate(Location location, string ReturningAction)
        {
            _repoLocation.ItemAdd(location);
            // get Referer
            //return Redirect(Request.Headers["Referer"].ToString());
            return RedirectToAction(ReturningAction);
        }

        public IActionResult LocationsList()
        {
            var list = _repoLocation.ItemsGetAll();
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
