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

namespace GloboDiet.Controllers
{
    public class HomeController : ControllerBase
    {

        public HomeController(IWebHostEnvironment webHostEnvironment, IRepository repo)
        {
            _webHostEnvironment = webHostEnvironment;
            _repo = repo;
        }
        //// GET: HomeController
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Test()
        {
            return Content(_repo.Test());
        }
        #region Respondent
        [HttpGet]
        public IActionResult CreateRespondent()
        {
            return View(new Respondent());
        }

        [HttpPost]
        public IActionResult CreateLocation(Respondent respondent)
        {
            _repo.AddRespondent(respondent);
            return Redirect("~/Home/Index");
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
            return View(new ViewModels.InterviewCreateEdit(new Interview(), _repo.GetAllLocations()));
        }

        [HttpPost]
        public IActionResult CreateInterview(Interview interview, string submit)
        {
            if (submit == "New Location")
                return Content("yeah");
            _repo.AddInterview(interview);
            return Redirect("~/Home/Index");
        }

        public IActionResult ListInterviews()
        {
            var list = _repo.GetAllInterviews();
            return View(list);
        }
        #endregion

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

    }
}
