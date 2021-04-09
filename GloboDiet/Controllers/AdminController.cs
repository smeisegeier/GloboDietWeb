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
    public class AdminController : _ControllerBase
    {

        public AdminController(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor, GloboDietDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContext = httpContextAccessor.HttpContext;
            _context = context;
        }

        public IActionResult Index()
        {
            return Content(_httpContext.Session.GetString("SessionUser"));
        }

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
            _context.ItemAdd<Interviewer>(interviewerCreateEdit);
            return RedirectToAction(nameof(InterviewersList));
        }

        [HttpGet]
        public IActionResult InterviewerEdit(int id)
        {
            InterviewerCreateEdit vm = _context.ItemGetById<Interviewer>(id);
            vm.Init(getNewNavigationBar(), Globals.ProcessMilestone._1_INTERVIEW);
            return View(vm);
        }

        [HttpPost]
        public IActionResult InterviewerEdit(InterviewerCreateEdit interviewerCreateEdit)
        {
            _context.ItemUpdate<Interviewer>(interviewerCreateEdit);
            return RedirectToAction(nameof(InterviewersList));
        }

        public IActionResult InterviewersList()
        {
            var list = _context.ItemsGetAll<Interviewer>();
            return View(new InterviewersList(list, getNewNavigationBar()));
        }

        public IActionResult InterviewerDetails(int id) => Json(_context.ItemGetById<Interviewer>(id));

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
            _context.ItemAdd<Location>(location);
            // get Referer
            //return Redirect(Request.Headers["Referer"].ToString());
            return RedirectToAction(ReturnAction);
        }

        [HttpGet]
        public IActionResult LocationEdit(int id)
        {
            LocationCreateEdit vm = _context.ItemGetById<Location>(id);
            vm.Init(getNewNavigationBar(), Globals.ProcessMilestone._1_INTERVIEW);
            return View(vm);
        }

        [HttpPost]
        public IActionResult LocationEdit(Location location)
        {
            _context.ItemUpdate<Location>(location);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        //public IActionResult LocationCreateToInterview() => View(new LocationCreateEdit(new Location(), getNewNavigationBar()));

        [HttpPost]
        public IActionResult LocationCreateToInterview(Location location)
        {
            _context.ItemAdd<Location>(location);
            return RedirectToAction("InterviewCreate", "Home");
        }


        public IActionResult LocationsList()
        {
            var list = _context.ItemsGetAll<Location>();
            return View(new LocationsList(list, getNewNavigationBar()));
        }
        public IActionResult LocationDetails(int id) => Json(_context.ItemGetById<Location>(id));


        #endregion

    }
}
