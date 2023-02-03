using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DextersLabor;
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
using System.Net;
using System.IO;

namespace GloboDiet.Controllers
{
    public class HomeController : _ControllerBase
    {

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

        [HttpGet]
        public IActionResult ImageSelector()
        {
            var fileArray = FileHelper.GetFileInfoFromDirectory(Path.Combine(_webHostEnvironment.WebRootPath, "images"));
            var imgList = new List<Image>();
            fileArray?.ToList().ForEach(src =>
                {
                    imgList.Add(new Image("/images/" + src.Name));
                });
            return View(new ImageSelector().Init(imgList, getNewNavigationBar()));
        }

        [HttpPost]
        public IActionResult ImageSelector4Create(MealElementCreateEdit mealElementCreateEdit)
        {
            var mealElement = mealElementCreateEdit;
            // 2) cache interview to register potential new Respondent
            _repoMealElement.ItemUpdate(mealElement);

            var fileArray = FileHelper.GetFileInfoFromDirectory(Path.Combine(_webHostEnvironment.WebRootPath, "images"));
            var imgList = new List<Image>();
            fileArray?.ToList().ForEach(src =>
            {
                imgList.Add(new Image("/images/" + src.Name));
            });
            return View(new ImageSelectorCreateEdit().Init(imgList, mealElement.Id, getNewNavigationBar()));
        }
        [HttpPost]
        public IActionResult ImageSelector4Save(string submit, ImageSelectorCreateEdit viewModel)
        {
            var mealElement = _repoMealElement.ItemGetById(viewModel.MealElementId);
            mealElement.ImagePath = submit;
            _repoMealElement.ItemUpdate(mealElement);
            return RedirectToAction(nameof(MealElement3Edit), new { id = viewModel.MealElementId });
        }


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
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return NotFound(); //Content("lol");
                //throw new Exception("Interview defect"); 
            }

            InterviewCreateEdit interviewCreateEdit = interviewNewOrFromDb;
            interviewCreateEdit.Init(
                _repoInterviewer.ItemsGetAll(),
                _repoLocation.ItemsGetAll(),
                getNewNavigationBar(),
                Globals.ProcessMilestone._1_INTERVIEW
                );
            return View(interviewCreateEdit);

        }
        // TODO Get-Clipboard | ConvertFrom-Json | ConvertTo-Json
        [HttpPost]
        public IActionResult Interview1Edit(InterviewCreateEdit interviewCreateEdit, string submit)
        {
            // cancel out immediately
            Interview interview = interviewCreateEdit;
            if (submit == "Cancel" && interview.IsCachedOnly)
            {
                _repoInterview.ItemDelete(interview);
            }
            else
            {
                // TODO ModelState checks https://blog.zhaytam.com/2019/04/13/asp-net-core-checking-modelstate-isvalid-is-boring/
                //if (interview.RespondentId == 0)
                //{ ModelState.AddModelError("CustomError", "No Respondent selected"); }
                //if (interview.InterviewerId == 0)
                //{ ModelState.AddModelError("CustomError", "No Interviewer selected"); }
                //if (interview.LocationId == 0)
                //{ ModelState.AddModelError("CustomError", "No Center selected"); }
                if (!ModelState.IsValid)
                {
                    return View(interviewCreateEdit);
                }
                // now save
                interview.IsCachedOnly = false;
                _repoInterview.ItemUpdate(interview);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Interview1List() => View(new InterviewsList(
            _repoInterview.ItemsGetAll(),
            getNewNavigationBar()
            ));

        public IActionResult Interview1Delete(int id)
        {
            _repoInterview.ItemDelete(id);
            return RedirectToAction(nameof(Interview1List));
        }

        public IActionResult Interview1Details(int id) => Json(_repoInterview.ItemGetById(id));

        #endregion

        #region Respondent

        [HttpPost]
        public IActionResult Respondent2Create(InterviewCreateEdit interviewCreateEdit)
        {
            Interview interview = interviewCreateEdit;
            // 1) ensure respondent
            if (interview.RespondentId is null || interview.RespondentId == 0)
                interview.RespondentId = _repoRespondent.ItemAdd(new Respondent(interview.Id));
            // 2) cache interview to register potential new Respondent
            _repoInterview.ItemUpdate(interview);
            return RedirectToAction(nameof(Respondent2Edit), new { id = interview.RespondentId });
        }

        [HttpGet]
        public IActionResult Respondent2Edit(int id)
        {
            RespondentCreateEdit respondentCreateEdit = _repoRespondent.ItemGetById(id);
            respondentCreateEdit.Init(getNewNavigationBar(), Globals.ProcessMilestone._1_INTERVIEW);
            return View(respondentCreateEdit);
        }

        [HttpPost]
        public IActionResult Respondent2Edit(RespondentCreateEdit respondentCreateEdit, string submit)
        {
            Respondent respondent = respondentCreateEdit;
            // special case: cancel -> no db operation
            if (submit != "Cancel")
            {
                _repoRespondent.ItemUpdate(respondent);
            }
            //_nLogger.Debug($"Label{model.Label}, Id {model.Id}, Interv {model.InterviewId}");
            return RedirectToAction(nameof(Interview1Edit), new { id = respondent.InterviewId });
        }

        public IActionResult Respondent2Details(int id) => Json(_repoRespondent.ItemGetById(id));
        #endregion

        #region Meal

        [HttpPost]
        public IActionResult Meal2Create(InterviewCreateEdit interviewCreateEdit)
        {
            var interview = interviewCreateEdit;
            // cache parent, then redirect
            _repoInterview.ItemUpdate(interview);
            // create new object w/ reference to parent
            var newMealId = _repoMeal.ItemAdd(new Meal(interview.Id));
            return RedirectToAction(nameof(Meal2Edit), new { id = newMealId });
        }

        [HttpGet]
        public IActionResult Meal2Edit(int id)
        {
            MealCreateEdit viewModel = _repoMeal.ItemGetById(id);
            viewModel.Init(getNewNavigationBar());
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Meal2Edit(MealCreateEdit mealCreateEdit, string submit)
        {
            Meal meal = mealCreateEdit;
            if (submit == "Cancel" && meal.IsCachedOnly)
            {
                _repoMeal.ItemDelete(meal);
            }
            else
            {
                meal.IsCachedOnly = false;
                _repoMeal.ItemUpdate(meal);
            }
            return RedirectToAction(nameof(Interview1Edit), new { id = meal.InterviewId });
        }

        public IActionResult Meal2Details(int id) => Json(_repoMeal.ItemGetById(id));

        public IActionResult Meal2Delete(int id)
        {
            var meal = _repoMeal.ItemGetById(id);
            _repoMeal.ItemDelete(meal);
            return RedirectToAction(nameof(Interview1Edit), new { id = meal.InterviewId });
        }

        #endregion

        #region MealElement
        [HttpPost]
        public IActionResult MealElement3Create(MealCreateEdit viewModel)
        {
            Meal model = viewModel;
            // cache parent, then redirect
            _repoMeal.ItemUpdate(model);
            // create new obj w/ reference to model
            var newMealElementId = _repoMealElement.ItemAdd(new MealElement(model.Id));
            return RedirectToAction(nameof(MealElement3Edit), new { id = newMealElementId });
        }

        [HttpGet]
        public IActionResult MealElement3Edit(int id)
        {
            //var mealElementNewOrFromDb = _repoMealElement.ItemGetById(id);
            //MealElementCreateEdit mealElementCreateEdit = mealElementNewOrFromDb;
            //mealElementCreateEdit.Init(getNewNavigationBar());
            //return View(mealElementCreateEdit);
            return View(((MealElementCreateEdit)_repoMealElement
                .ItemGetById(id)
                )
                .Init(getNewNavigationBar())
                );
        }

        [HttpPost]
        public IActionResult MealElement3Edit(MealElementCreateEdit viewModel, string submit)
        {
            MealElement model = viewModel;
            if (submit == "Cancel" && model.IsCachedOnly)
            {
                _repoMealElement.ItemDelete(model);
            }
            else
            {
                model.IsCachedOnly = false;
                _repoMealElement.ItemUpdate(model);
            }
            return RedirectToAction(nameof(Meal2Edit), new { id = model.MealId });
        }

        public IActionResult MealElement3Delete(int id)
        {
            var mealElement = _repoMealElement.ItemGetById(id);
            _repoMealElement.ItemDelete(mealElement);
            return RedirectToAction(nameof(Meal2Edit), new { id = mealElement.MealId });
        }
        public IActionResult MealElement3Details(int id) => Json(_repoMealElement.ItemGetById(id));


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
