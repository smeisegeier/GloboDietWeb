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
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace GloboDiet.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static NLog.Logger _nLogger = NLog.LogManager.GetCurrentClassLogger();
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

            _nLogger.Info("Controller started");
            seedAll();
        }

        // TODO use modal window instead of status area
        [AllowAnonymous]
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
        [HttpPost]
        public IActionResult RespondentCreate(Respondent respondent)
        {
            // TODO insert checks
            _repoRespondent.ItemAdd(respondent);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult RespondentEdit(int id)
        {
            // TODO checks
            var respondent = _repoRespondent.ItemGetById(id);
            return View(new RespondentCreateEdit(respondent, getNewNavigationBar()));
        }
        [HttpPost]
        public IActionResult RespondentEdit(Respondent respondent)
        {
            if (respondent.Weight > 80)
            {
                ModelState.AddModelError("CustomError", "too schwer");
                ModelState.AddModelError("CustomError", "lol");
            }

            if (!ModelState.IsValid)
                return View(new RespondentCreateEdit(respondent, getNewNavigationBar()));


            _repoRespondent.ItemUpdate(respondent);
            return RedirectToAction(nameof(RespondentsList));
        }

        public IActionResult RespondentsList()
        {
            var list = _repoRespondent.ItemsGetAll();
            return View(new RespondentsList(list, getNewNavigationBar()));
        }
        public IActionResult RespondentDetails(int id) => Json(_repoRespondent.ItemGetById(id));

        #endregion

        #region Interview
        [HttpGet]
        public IActionResult InterviewCreate()
        {
            var modelNewOrEmpty = new Interview();
            if (_httpContext.Session.GetString("InterviewCache") is not null)
            {
                modelNewOrEmpty = JsonConvert.DeserializeObject<Interview>(_httpContext.Session.GetString("InterviewCache"));
            }

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
        public IActionResult InterviewCreateToLocation(Interview interview)
        {
            // cache object, then redirect
            _httpContext.Session.SetString("InterviewCache", JsonConvert.SerializeObject(interview));

            // TODO remove 
            //return RedirectToAction(nameof(LocationCreate), new { ReturnAction = nameof(InterviewCreate) });
            return RedirectToAction(nameof(LocationCreateToInterview));
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
            // TODO custom validation as in PlanungsTool?
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

        /* admin */

        #region Interviewer

        [Authorize]
        [HttpGet]
        public IActionResult InterviewerCreate()
        {
            return View(new InterviewerCreateEdit( new Interviewer(), getNewNavigationBar()));
        }

        [HttpPost]
        public IActionResult InterviewerCreate(Interviewer interviewer)
        {
            if (!ModelState.IsValid) return View(interviewer);
            _repoInterviewer.ItemAdd(interviewer);
            return Redirect("~/Home/Index");
        }

        [HttpGet]
        public IActionResult InterviewerEdit(int id)
        {
            var interviewer = _repoInterviewer.ItemGetById(id);
            return View(new InterviewerCreateEdit(interviewer, getNewNavigationBar()));
        }

        [HttpPost]
        public IActionResult InterviewerEdit(Interviewer interviewer)
        {
            // TODO custom validation as in PlanungsTool?
            _repoInterviewer.ItemUpdate(interviewer);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult InterviewersList()
        {
            var list = _repoInterviewer.ItemsGetAll();
            return View(new InterviewersList(list, getNewNavigationBar()));
        }

        public IActionResult InterviewerDetails(int id) => Json(_repoInterviewer.ItemGetById(id));

        #endregion

        #region Location
        [HttpGet]
        public IActionResult LocationCreate(string returnAction=null)
        {
            return View(new ViewModels.LocationCreateEdit(new Location(), getNewNavigationBar(), returnAction));
        }
        [HttpPost]
        public IActionResult LocationCreate(Location location, string ReturnAction)
        {
            _repoLocation.ItemAdd(location);
            // get Referer
            //return Redirect(Request.Headers["Referer"].ToString());
            return RedirectToAction(ReturnAction);
        }

        [HttpGet]
        public IActionResult LocationEdit(int id ) => View(new LocationCreateEdit(_repoLocation.ItemGetById(id), getNewNavigationBar()));

        [HttpPost]
        public IActionResult LocationEdit(Location location)
        {
            _repoLocation.ItemUpdate(location);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult LocationCreateToInterview() => View(new LocationCreateEdit(new Location(), getNewNavigationBar()));

        [HttpPost]
        public IActionResult LocationCreateToInterview(Location location)
        {
            _repoLocation.ItemAdd(location);
            return RedirectToAction(nameof(InterviewCreate));
        }


        public IActionResult LocationsList()
        {
            var list = _repoLocation.ItemsGetAll();
            return View(new LocationsList(list, getNewNavigationBar()));
        }
        public IActionResult LocationDetails(int id) => Json(_repoLocation.ItemGetById(id));


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
