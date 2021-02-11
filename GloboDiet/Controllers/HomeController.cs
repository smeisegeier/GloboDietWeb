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
            return View(new _ViewModelBase(getNewNavigationBar()));
        }


        #region Private Area
        private NavigationBar getNewNavigationBar() => new NavigationBar(_repoInterview.ItemsGetCount(), _repoInterviewer.ItemsGetCount(), _repoLocation.ItemsGetCount(), _repoRespondent.ItemsGetCount(), _repoInterview.GetSqlConnectionType());

        #endregion

        #region 02x

        [HttpGet]
        public IActionResult NewInterview020Create()
        {
            var newId = _repoInterview.ItemAdd(new Interview());
            return RedirectToAction(nameof(NewInterview020), new { id = newId });
        }

        [HttpGet]
        public IActionResult NewInterview020(int id)
        {
            var interviewNewOrFromDb = _repoInterview.ItemGetById(id);
            if (interviewNewOrFromDb is not Interview)
                { throw new Exception("Interview defect"); }

            var newModel = new InterviewCreateEdit(
                interviewNewOrFromDb,
                _repoInterviewer.ItemsGetAll(),
                _repoLocation.ItemsGetAll(),
                Globals.ProcessMilestone._1_INTERVIEW,
                getNewNavigationBar()
                );
            return View(newModel);

        }
        // TODO Get-Clipboard | ConvertFrom-Json | ConvertTo-Json
        // TODO use session Id
        [HttpPost]
        public IActionResult NewInterview020(Interview model)
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
                    Globals.ProcessMilestone._1_INTERVIEW,
                    getNewNavigationBar()
                    ));
            }

            _repoInterview.ItemUpdate(model);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// xx0 -> POST -> xx1, because the object of xx0 (interview) must now be cached
        /// xx1 ist just relay
        /// </summary>
        /// <param name="interview"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult NewInterview021(Interview model)
        {
            // 1) ensure respondent
            if (model.RespondentId is null || model.RespondentId == 0)
                model.RespondentId = _repoRespondent.ItemAdd(new Respondent(model.Id));
            // 2) cache interview to register potential new Respondent
            _repoInterview.ItemUpdate(model);
            return RedirectToAction(nameof(NewInterview022), new { id = model.RespondentId });
        }

        // xx1 -> GET -> xx2
        /// <summary>
        /// CreateOrEdit _respondent. Called from xx1
        /// </summary>
        /// <param name="id">Id of respondent object.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult NewInterview022(int id)
        {
            var respondentFromDb = _repoRespondent.ItemGetById(id);
            return View(new RespondentCreateEdit(
                respondentFromDb, 
                getNewNavigationBar(), 
                Globals.ProcessMilestone._1_INTERVIEW
                ));
        }

        // xx2 -> GET xx0 (save)
        // xx2 -> GET xx0 (cancel)
        [HttpPost]
        public IActionResult NewInterview022(Respondent model, string submit)
        {
            if (submit != "Cancel")
                { _repoRespondent.ItemUpdate(model); }
            //_nLogger.Debug($"Label{model.Label}, Id {model.Id}, Interv {model.InterviewId}");
            return RedirectToAction(nameof(NewInterview020), new { id = model.InterviewId });
        }
        #endregion

        #region 04x

        [HttpPost]
        public IActionResult NewInterview041(Interview model)
        {
            // cache object, then redirect
            _repoInterview.ItemUpdate(model);
            // create new meal w/ reference to parent
            var newMealId = _repoMeal.ItemAdd(new Meal(model.Id));
            return RedirectToAction(nameof(NewInterview042), new { id = newMealId });
        }

        /// <summary>
        /// CreateOrEdit _meal
        /// </summary>
        /// <param name="id">id of meal</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult NewInterview042(int id)
        {
            var mealNewOrFromDb = _repoMeal.ItemGetById(id);
            return View(new MealCreateEdit(mealNewOrFromDb, getNewNavigationBar()));
        }

        [HttpPost]
        public IActionResult NewInterview042(Meal model, string submit)
        {
            if (submit != "Cancel")
                { _repoMeal.ItemAddOrUpdate(model); }
            return RedirectToAction(nameof(NewInterview020), new { id = model.InterviewId });
        }
        #endregion


        // TODO fix
        [HttpPost]
        public IActionResult CreateEditMealElement_PRE(Meal model)
        {
            // cache object, then redirect
            _repoMeal.ItemUpdate(model);
            // create new mealele w/ reference to parent
            var newMealElementId = _repoMealElement.ItemAdd(new MealElement(model.Id));
            return RedirectToAction(nameof(CreateEditMealElement), new { id = newMealElementId });
        }

        [HttpGet]
        public IActionResult CreateEditMealElement(int id)
        {
            var mealElementNewOrFromDb = _repoMealElement.ItemGetById(id);
            return View(new MealElementCreateEdit(mealElementNewOrFromDb, getNewNavigationBar()));
        }

        [HttpPost]
        public IActionResult CreateEditMealElement(MealElement model, string submit)
        {
            if (submit != "Cancel")
            { _repoMealElement.ItemAddOrUpdate(model); }
            return RedirectToAction(nameof(NewInterview042), new { id = model.MealId });
        }


        #region Respondent
        [HttpGet]
        public IActionResult RespondentCreate()
        {
            return View(new RespondentCreateEdit(new Respondent(), getNewNavigationBar(), Globals.ProcessMilestone._1_INTERVIEW));
        }
        [HttpPost]
        public IActionResult RespondentCreate(Respondent respondent)
        {
            _repoRespondent.ItemAdd(respondent);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult RespondentEdit(int id)
        {
            var respondent = _repoRespondent.ItemGetById(id);
            return View(new RespondentCreateEdit(respondent, getNewNavigationBar(), Globals.ProcessMilestone._1_INTERVIEW));
        }
        [HttpPost]
        public IActionResult RespondentEdit(Respondent respondent)
        {
            if (respondent.Weight > 80)
            {
                ModelState.AddModelError("CustomError", "too schwer");
            }
            if (respondent.Height > 200)
            {
                ModelState.AddModelError("CustomError", "too hoch");
            }

            if (!ModelState.IsValid)
                return View(new RespondentCreateEdit(respondent, getNewNavigationBar(), Globals.ProcessMilestone._1_INTERVIEW));


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
