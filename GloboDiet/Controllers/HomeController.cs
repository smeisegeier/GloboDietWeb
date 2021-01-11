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
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepository _repo;

        public HomeController(IWebHostEnvironment webHostEnvironment, IRepository repo)
        {
            _webHostEnvironment = webHostEnvironment;
            _repo = repo;
        }

        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return Content(_repo.Test());
        }

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

        public IActionResult ListRespondent()
        {
            var list = _repo.GetAllRespondents();
            return View(list);
        }

        [HttpGet]
        public IActionResult CreateInterview()
        {
            return View(new Interview());
        }

        [HttpPost]
        public IActionResult CreateInterview(Interview interview)
        {
            _repo.AddInterview(interview);
            return Redirect("~/Home/Index");
        }

        public IActionResult ListInterviews()
        {
            var list = _repo.GetAllInterviews();
            return View(list);
        }

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

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
