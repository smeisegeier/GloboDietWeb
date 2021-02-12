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
using GloboDiet.Extensions;

namespace GloboDiet.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private static NLog.Logger _nLogger = NLog.LogManager.GetCurrentClassLogger();
        private readonly HttpContext _httpContext;
        private readonly LookupData _lookupData;

        // example for domain repo
        private readonly IRepositoryNew<Interview> _repoInterview;
        private readonly IRepositoryNew<Interviewer> _repoInterviewer;
        private readonly IRepositoryNew<Location> _repoLocation;
        private readonly IRepositoryNew<Respondent> _repoRespondent;
        private readonly IRepositoryNew<Meal> _repoMeal;
        private readonly IRepositoryNew<MealElement> _repoMealElement;

        public HomeController(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            LookupData lookupData,
            IRepositoryNew<Interview> repoInterview,
            IRepositoryNew<Interviewer> repoInterviewer,
            IRepositoryNew<Location> repoLocation,
            IRepositoryNew<Respondent> repoRespondent,
            IRepositoryNew<Meal> repoMeal,
            IRepositoryNew<MealElement> repoMealElement
            )
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContext = httpContextAccessor.HttpContext;
            _lookupData = lookupData;
            _repoInterview = repoInterview;
            _repoInterviewer = repoInterviewer;
            _repoLocation = repoLocation;
            _repoRespondent = repoRespondent;
            _repoMeal = repoMeal;
            _repoMealElement = repoMealElement;
        }

        // TODO use modal window instead of status area
        [AllowAnonymous]
        public IActionResult Index()
        {
            // if no content needed just pass _ViewModelBase
            //return View(new _ViewModelBase(getNewNavigationBar()));
            return RedirectToActionPermanent(nameof(Interview1List));
        }


        #region Private Area
        private NavigationBar getNewNavigationBar() => new NavigationBar(_repoInterview.ItemsGetCount(), _repoInterviewer.ItemsGetCount(), _repoLocation.ItemsGetCount(), _repoRespondent.ItemsGetCount(), _repoInterview.GetSqlConnectionType());

        #endregion

        #region Interview

        [HttpGet]
        public IActionResult Interview1Create()
        {
            var newId = _repoInterview.ItemAdd(new Interview());
            return RedirectToAction(nameof(Interview1Edit), new { id = newId });
        }

        [HttpGet]
        public IActionResult Interview1Edit(int id)
        {
            var interviewNewOrFromDb = _repoInterview.ItemGetById(id);
            if (interviewNewOrFromDb is not Interview)
                { throw new Exception("Interview defect"); }

            var newModel = new InterviewCreateEdit(
                interviewNewOrFromDb,
                _repoInterviewer.ItemsGetAll(),
                _repoLocation.ItemsGetAll(),
                getNewNavigationBar()
                );
            return View(newModel);

        }
        // TODO Get-Clipboard | ConvertFrom-Json | ConvertTo-Json
        // TODO use session Id
        [HttpPost]
        public IActionResult Interview1Edit(Interview model)
        {
            //if (interview.RespondentId == 0)
            //{ ModelState.AddModelError("CustomError", "No Respondent selected"); }
            //if (interview.InterviewerId == 0)
            //{ ModelState.AddModelError("CustomError", "No Interviewer selected"); }
            //if (interview.LocationId == 0)
            //{ ModelState.AddModelError("CustomError", "No Center selected"); }

            if (!ModelState.IsValid)
            {
                // TODO create helper method
                return View(new InterviewCreateEdit(
                    model,
                    _repoInterviewer.ItemsGetAll(),
                    _repoLocation.ItemsGetAll(),
                    getNewNavigationBar()
                    ));
            }

            _repoInterview.ItemUpdate(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Interview1List() => View(new InterviewsList(
            _repoInterview.ItemsGetAll(),
            getNewNavigationBar()
            ));

        public IActionResult Interview1Details(int id) => Json(_repoInterview.ItemGetById(id));

        #endregion

        #region Respondent

        [HttpPost]
        public IActionResult Respondent2Create(Interview model)
        {
            // 1) ensure respondent
            if (model.RespondentId is null || model.RespondentId == 0)
                model.RespondentId = _repoRespondent.ItemAdd(new Respondent(model.Id));
            // 2) cache interview to register potential new Respondent
            _repoInterview.ItemUpdate(model);
            return RedirectToAction(nameof(Respondent2Edit), new { id = model.RespondentId });
        }

        [HttpGet]
        public IActionResult Respondent2Edit(int id)
        {
            var respondentFromDb = _repoRespondent.ItemGetById(id);
            return View(new RespondentCreateEdit(
                respondentFromDb, 
                getNewNavigationBar(), 
                Globals.ProcessMilestone._1_INTERVIEW
                ));
        }

        [HttpPost]
        public IActionResult Respondent2Edit(Respondent model, string submit)
        {
            if (submit != "Cancel")
                { _repoRespondent.ItemUpdate(model); }
            //_nLogger.Debug($"Label{model.Label}, Id {model.Id}, Interv {model.InterviewId}");
            return RedirectToAction(nameof(Interview1Edit), new { id = model.InterviewId });
        }

        public IActionResult Respondent2Details(int id) => Json(_repoRespondent.ItemGetById(id));

        #endregion

        #region Meal

        [HttpPost]
        public IActionResult Meal2Create(Interview model)
        {
            // cache parent, then redirect
            _repoInterview.ItemUpdate(model);
            // create new object w/ reference to parent
            var newMealId = _repoMeal.ItemAdd(new Meal(model.Id));
            return RedirectToAction(nameof(Meal2Edit), new { id = newMealId });
        }

        [HttpGet]
        public IActionResult Meal2Edit(int id)
        {
            var mealNewOrFromDb = _repoMeal.ItemGetById(id);
            return View(new MealCreateEdit(mealNewOrFromDb, getNewNavigationBar()));
        }

        [HttpPost]
        public IActionResult Meal2Edit(Meal model, string submit)
        {
            if (submit != "Cancel")
                { _repoMeal.ItemAddOrUpdate(model); }
            return RedirectToAction(nameof(Interview1Edit), new { id = model.InterviewId });
        }

        public IActionResult Meal2Details(int id) => Json(_repoMeal.ItemGetById(id));

        #endregion

        #region MealElement
        [HttpPost]
        public IActionResult MealElement3Create(Meal model)
        {
            // cache parent, then redirect
            _repoMeal.ItemUpdate(model);
            // create new obj w/ reference to model
            var newMealElementId = _repoMealElement.ItemAdd(new MealElement(model.Id));
            return RedirectToAction(nameof(MealElement3Edit), new { id = newMealElementId });
        }

        [HttpGet]
        public IActionResult MealElement3Edit(int id)
        {
            var mealElementNewOrFromDb = _repoMealElement.ItemGetById(id);
            return View(new MealElementCreateEdit(mealElementNewOrFromDb, getNewNavigationBar()));
        }

        [HttpPost]
        public IActionResult MealElement3Edit(MealElement model, string submit)
        {
            // TODO cancel triggers validation..
            if (submit != "Cancel")
            { _repoMealElement.ItemAddOrUpdate(model); }
            return RedirectToAction(nameof(Meal2Edit), new { id = model.MealId });
        }

        #endregion

        // TODO Edit / details -> template? css? view?

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
