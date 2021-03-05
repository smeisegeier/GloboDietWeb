using GloboDiet.Models;
using GloboDiet.Services;
using GloboDiet.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Controllers
{
    //[Authorize]
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly HttpContext _httpContext;

        private readonly IRepositoryNew<Interviewer> _repoInterviewer;
        private readonly IRepositoryNew<Location> _repoLocation;

        public AdminController(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, IRepositoryNew<Interviewer> repoInterviewer, IRepositoryNew<Location> repoLocation)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContext = httpContextAccessor.HttpContext;
            _repoInterviewer = repoInterviewer;
            _repoLocation = repoLocation;
        }

        public IActionResult Index()
        {
            return Content(_httpContext.Session.GetString("SessionUser"));
        }

        #region private area
        //private NavigationBar getNewNavigationBar() => new NavigationBar(0, _repoInterviewer.ItemsGetCount(), _repoLocation.ItemsGetCount(), 0, 0);
        private NavigationBar getNewNavigationBar() => new NavigationBar()
        {
            CurrentSqlConnectionType = _repoInterviewer.GetSqlConnectionType(),
            PillCountInterviewers = _repoInterviewer.ItemsGetCount(),
            PillCountLocations = _repoLocation.ItemsGetCount()  
        };

        #endregion

        #region Interviewer

        [HttpGet]
        public IActionResult InterviewerCreate()
        {
            InterviewerCreateEdit vm = new Interviewer();
            vm.Init(getNewNavigationBar(), Globals.ProcessMilestone._1_INTERVIEW);
            return View(vm);
        }

        [HttpPost]
        public IActionResult InterviewerCreate(InterviewerCreateEdit interviewerCreateEdit)
        {
            if (!ModelState.IsValid) 
                return View(interviewerCreateEdit);
            _repoInterviewer.ItemAdd(interviewerCreateEdit);
            return RedirectToAction(nameof(InterviewersList));
        }

        [HttpGet]
        public IActionResult InterviewerEdit(int id)
        {
            InterviewerCreateEdit vm = _repoInterviewer.ItemGetById(id);
            vm.Init(getNewNavigationBar(), Globals.ProcessMilestone._1_INTERVIEW);
            return View(vm);
        }

        [HttpPost]
        public IActionResult InterviewerEdit(InterviewerCreateEdit interviewerCreateEdit)
        {
            _repoInterviewer.ItemUpdate(interviewerCreateEdit);
            return RedirectToAction(nameof(InterviewersList));
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
        public IActionResult LocationCreate()
        {
            LocationCreateEdit vm = new LocationCreateEdit();
            vm.Init(getNewNavigationBar(), Globals.ProcessMilestone._1_INTERVIEW);
            return View(vm);
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
        public IActionResult LocationEdit(int id)
        {
            LocationCreateEdit vm = _repoLocation.ItemGetById(id);
            vm.Init(getNewNavigationBar(), Globals.ProcessMilestone._1_INTERVIEW);
            return View(vm);
        }

        [HttpPost]
        public IActionResult LocationEdit(Location location)
        {
            _repoLocation.ItemUpdate(location);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        //public IActionResult LocationCreateToInterview() => View(new LocationCreateEdit(new Location(), getNewNavigationBar()));

        [HttpPost]
        public IActionResult LocationCreateToInterview(Location location)
        {
            _repoLocation.ItemAdd(location);
            return RedirectToAction("InterviewCreate", "Home");
        }


        public IActionResult LocationsList()
        {
            var list = _repoLocation.ItemsGetAll();
            return View(new LocationsList(list, getNewNavigationBar()));
        }
        public IActionResult LocationDetails(int id) => Json(_repoLocation.ItemGetById(id));


        #endregion

    }
}
