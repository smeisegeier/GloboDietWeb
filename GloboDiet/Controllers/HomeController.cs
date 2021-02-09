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

        // example for domain repo
        private readonly IRepositoryNew<Interview> _repoInterview;
        private readonly IRepositoryNew<Interviewer> _repoInterviewer;
        private readonly IRepositoryNew<Location> _repoLocation;
        private readonly IRepositoryNew<Respondent> _repoRespondent;
        private readonly IRepositoryNew<Recipe> _repoRecipe;
        private readonly IRepositoryNew<Meal> _repoMeal;
        private readonly IRepositoryNew<MealType> _repoMealType;
        private readonly IRepositoryNew<MealPlace> _repoMealPlace;
        private readonly IRepositoryNew<Brandname> _repoBrandname;

        public HomeController(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            IRepositoryNew<Interview> repoInterview,
            IRepositoryNew<Interviewer> repoInterviewer,
            IRepositoryNew<Location> repoLocation,
            IRepositoryNew<Respondent> repoRespondent,
            IRepositoryNew<Recipe> repoRecipe,
            IRepositoryNew<Meal> repoMeal,
            IRepositoryNew<MealType> repoMealType,
            IRepositoryNew<MealPlace> repoMealPlace,
            IRepositoryNew<Brandname> repoBrandname
            )
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContext = httpContextAccessor.HttpContext;
            _repoInterview = repoInterview;
            _repoInterviewer = repoInterviewer;
            _repoLocation = repoLocation;
            _repoRespondent = repoRespondent;
            _repoRecipe = repoRecipe;
            _repoMeal = repoMeal;
            _repoMealType = repoMealType;
            _repoMealPlace = repoMealPlace;
            _repoBrandname = repoBrandname;
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
            if (interviewNewOrFromDb is null)
                throw new Exception("Interview defect");

            var newModel = new InterviewCreateEdit(
            interviewNewOrFromDb,
            _repoInterviewer.ItemsGetAll(),
            _repoLocation.ItemsGetAll(),
            _repoMeal.ItemsGetAll(),
            Globals.ProcessMilestone._1_INTERVIEW,
            getNewNavigationBar()
            );
            return View(newModel);

        }
        // TODO Get-Clipboard | ConvertFrom-Json | ConvertTo-Json

        [HttpPost]
        public IActionResult NewInterview020(Interview model)
        {
            //if (interview.RespondentId == 0)
            //{ ModelState.AddModelError("CustomError", "No _respondent selected"); }
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
                _repoMeal.ItemsGetAll(),
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
            var interview = model;
            if (interview.RespondentId is null || interview.RespondentId == 0)
                interview.RespondentId = _repoRespondent.ItemAdd(new Respondent());
 
            _repoInterview.ItemUpdate(interview);

            return RedirectToAction(nameof(NewInterview022), new { id = interview.Id });
        }

        // xx1 -> GET -> xx2
        /// <summary>
        /// CreateOrEdit _respondent. Called from xx1
        /// </summary>
        /// <param name="id">Id of _respondent object.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult NewInterview022(int id)
        {
            var interviewFromDB = _repoInterview.ItemGetById(id);
            var respondentFromDb = _repoRespondent.ItemGetById((int)interviewFromDB.RespondentId);
            
            // HACK manual override - w/o it Id's are not working
            respondentFromDb.InterviewId = interviewFromDB.Id;

            return View(new RespondentCreateEdit(respondentFromDb, getNewNavigationBar(), Globals.ProcessMilestone._1_INTERVIEW));
        }

        // xx2 -> GET xx0 (save)
        // xx2 -> GET xx0 (cancel)
        [HttpPost]
        public IActionResult NewInterview022(Respondent model, string submit)
        {
            if (submit!="Cancel")
                _repoRespondent.ItemUpdate(model);
            _nLogger.Debug($"Label{model.Label}, Id {model.Id}, Interv {model.InterviewId}");
            return RedirectToAction(nameof(NewInterview020), new { id = model.InterviewId });
        }
        #endregion

        #region 04x
        [HttpPost]
        public IActionResult NewInterview041(Interview model)
        {
            // cache object, then redirect
            _repoInterview.ItemUpdate(model);
            return RedirectToAction(nameof(NewInterview042), new { id = 0 });
        }

        /// <summary>
        /// CreateOrEdit Meal
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult NewInterview042(int id)
        {
            var mealNewOrFromDb = new Meal();
            if (id != 0)
            { mealNewOrFromDb = _repoMeal.ItemGetById(id); }
            return View(new MealCreateEdit(mealNewOrFromDb, getNewNavigationBar()));
        }

        [HttpPost]
        public IActionResult NewInterview042(Meal meal)
        {
            _repoMeal.ItemAddOrUpdate(meal);
            //_nLogger.Debug(_repoMeal.ItemsGetCount());
            //_nLogger.Debug(_repoMeal.ItemsGetAll().ToList().Select(s=>s.Label));
            return RedirectToAction(nameof(NewInterview020));
        }
        #endregion


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

        #region Interview


        // TODO Edit / details -> template? css? view?

        public IActionResult InterviewsList()
        {
            var list = _repoInterview.ItemsGetAll();
            return View(new InterviewsList(list, getNewNavigationBar()));
        }

        public IActionResult InterviewDetails(int id) => Json(_repoInterview.ItemGetById(id));

        #endregion

        #region Recipe
        [HttpGet]
        public IActionResult RecipeCreate()
        {
            var modelNewOrEmpty = new Recipe();
            return View(new RecipeCreateEdit(modelNewOrEmpty, getNewNavigationBar()));
        }

        [HttpPost]
        public IActionResult RecipeCreate(Recipe recipe)
        {
            _repoRecipe.ItemAdd(recipe);
            return RedirectToAction(nameof(RecipesList));
        }

        [HttpGet]
        public IActionResult RecipeEdit(int id)
        {
            var recipe = _repoRecipe.ItemGetById(id);
            return View(new RecipeCreateEdit(recipe, getNewNavigationBar()));
        }

        [HttpPost]
        public IActionResult RecipeEdit(Recipe recipe)
        {
            _repoRecipe.ItemUpdate(recipe);
            return RedirectToAction(nameof(RecipesList));
        }

        public IActionResult RecipesList()
        {
            var list = _repoRecipe.ItemsGetAll();
            return View(new RecipesList(list, getNewNavigationBar()));
        }

        public IActionResult RecipeDetails(int id) => Json(_repoRecipe.ItemGetById(id));


        #endregion


        /* admin */

        #region Interviewer

        [HttpGet]
        public IActionResult InterviewerCreate()
        {
            return View(new InterviewerCreateEdit(new Interviewer(), getNewNavigationBar()));
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
        public IActionResult LocationCreate(string returnAction = null)
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
        public IActionResult LocationEdit(int id) => View(new LocationCreateEdit(_repoLocation.ItemGetById(id), getNewNavigationBar()));

        [HttpPost]
        public IActionResult LocationEdit(Location location)
        {
            _repoLocation.ItemUpdate(location);
            return RedirectToAction(nameof(Index));
        }


        //[HttpGet]
        //public IActionResult LocationCreateToInterview() => View(new LocationCreateEdit(new Location(), getNewNavigationBar()));

        //[HttpPost]
        //public IActionResult LocationCreateToInterview(Location location)
        //{
        //    _repoLocation.ItemAdd(location);
        //    return RedirectToAction(nameof(InterviewCreate));
        //}


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
