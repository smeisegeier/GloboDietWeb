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
            GloboDietDbContext context
            )
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContext = httpContextAccessor.HttpContext;
            _lookupData = lookupData;
            _context = context;
        }


        // TODO use modal window instead of status area
        [AllowAnonymous]
        public IActionResult Index()
        {
            // if no content needed just pass _ViewModelBase
            //return View(new _ViewModelBase(getNewNavigationBar()));
            return RedirectToActionPermanent(nameof(Interview1List));
        }


        [HttpPost]
        public IActionResult ImageSelector4Create(MealElementCreateEdit mealElementCreateEdit)
        {
            var mealElement = mealElementCreateEdit;
            // 2) cache interview to register potential new Respondent
            _context.ItemUpdate<MealElement>(mealElement);

            //var fileArray = FileHelper.GetFileInfoFromDirectory(Path.Combine(_webHostEnvironment.WebRootPath, "images"));
            //var imgList = new List<Image>();
            //fileArray?.ToList().ForEach(src =>
            //{
            //    imgList.Add(new Image("/images/" + src.Name));
            //});
            //return View(new ImageSelectorCreateEdit().Init(imgList, mealElement.Id, getNewNavigationBar()));
            return View(new ImageSelectorCreateEdit().Init(_lookupData.ListOfAllFoodImages, mealElement.Id, getNewNavigationBar()));

        }
        [HttpPost]
        public IActionResult ImageSelector4Save(int submit, ImageSelectorCreateEdit viewModel)
        {
            var mealElement = _context.ItemGetById<MealElement>(viewModel.MealElementId);
            mealElement.FoodImageId = submit;
            _context.ItemUpdate<MealElement>(mealElement);
            return RedirectToAction(nameof(MealElement3Edit), new { id = viewModel.MealElementId });
        }


        #region Interview

        [HttpGet]
        public IActionResult Interview1Create()
        {
            var newId = _context.ItemAdd<Interview>(new Interview());
            return RedirectToAction(nameof(Interview1Edit), new { id = newId });
        }

        [HttpGet]
        public IActionResult Interview1Edit(int id)
        {
            //var interviewNewOrFromDb = _context.ItemGetById<Interview>(id);
            //var interviewNewOrFromDb = _context.Set<Interview>()
            //    .Include(i => i.Respondent)
            //    // inlude also deeper level of model
            //    .Include(i => i.Meals).ThenInclude(i => i.MealPlace)
            //    .Include(i => i.Meals).ThenInclude(i => i.MealType)
            //    .ToList()
            //    .FirstOrDefault(x => x.Id == id);
            var interviewNewOrFromDb = _context.ItemGetById<Interview>(id);

            if (interviewNewOrFromDb is not Interview)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return NotFound(); //Content("lol");
                //throw new Exception("Interview defect"); 
            }

            InterviewCreateEdit interviewCreateEdit = interviewNewOrFromDb;
            interviewCreateEdit.Init(
                _context.ItemsGetAll<Interviewer>(),
                _context.ItemsGetAll<Location>(),
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
                _context.ItemDelete<Interview>(interview);
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
                _context.ItemUpdate<Interview>(interview);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Interview1List() => View(new InterviewsList(
            _context.ItemsGetAll<Interview>(),
            getNewNavigationBar()
            ));

        public IActionResult Interview1Delete(int id)
        {
            _context.ItemDelete<Interview>(_context.ItemGetById<Interview>(id));
            return RedirectToAction(nameof(Interview1List));
        }

        public IActionResult Interview1Details(int id) => Json(_context.ItemGetById<Interview>(id));

        public IActionResult Interview1Xml(int id) => Content(
            _context.ItemGetById<Interview>(id)
            .ToXml());

        #endregion
        #region Respondent

        [HttpPost]
        public IActionResult Respondent2Create(InterviewCreateEdit interviewCreateEdit)
        {
            Interview interview = interviewCreateEdit;
            // 1) ensure respondent
            if (interview.RespondentId is null || interview.RespondentId == 0)
                interview.RespondentId = _context.ItemAdd<Respondent>(new Respondent(interview.Id));
            // 2) cache interview to register potential new Respondent
            _context.ItemUpdate<Interview>(interview);
            return RedirectToAction(nameof(Respondent2Edit), new { id = interview.RespondentId });
        }

        [HttpGet]
        public IActionResult Respondent2Edit(int id)
        {
            RespondentCreateEdit respondentCreateEdit = _context.ItemGetById<Respondent>(id);
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
                _context.ItemUpdate<Respondent>(respondent);
            }
            //_nLogger.Debug($"Label{model.Label}, Id {model.Id}, Interv {model.InterviewId}");
            return RedirectToAction(nameof(Interview1Edit), new { id = respondent.InterviewId });
        }

        public IActionResult Respondent2Details(int id) => Json(_context.ItemGetById<Respondent>(id));
        #endregion

        #region Meal

        [HttpPost]
        public IActionResult Meal2Create(InterviewCreateEdit interviewCreateEdit)
        {
            var interview = interviewCreateEdit;
            // cache parent, then redirect
            _context.ItemUpdate<Interview>(interview);
            // create new object w/ reference to parent
            var newMealId = _context.ItemAdd<Meal>(new Meal(interview.Id));
            return RedirectToAction(nameof(Meal2Edit), new { id = newMealId });
        }

        [HttpGet]
        public IActionResult Meal2Edit(int id)
        {
            MealCreateEdit viewModel = _context.ItemGetById<Meal>(id);
            //MealCreateEdit viewModel = _context.Set<Meal>()
            //    .Include(i => i.MealElements)
            //    .ThenInclude(i => i.Ingredient)
            //    .ToList()
            //    .FirstOrDefault(x => x.Id == id);

            viewModel.Init(getNewNavigationBar());
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Meal2Edit(MealCreateEdit mealCreateEdit, string submit)
        {
            Meal meal = mealCreateEdit;
            if (submit == "Cancel" && meal.IsCachedOnly)
            {
                _context.ItemDelete<Meal>(meal);
            }
            else
            {
                meal.IsCachedOnly = false;
                _context.ItemUpdate<Meal>(meal);
            }
            return RedirectToAction(nameof(Interview1Edit), new { id = meal.InterviewId });
        }

        public IActionResult Meal2Details(int id) => Json(_context.ItemGetById<Meal>(id));

        public IActionResult Meal2Delete(int id)
        {
            var meal = _context.ItemGetById<Meal>(id);
            _context.ItemDelete<Meal>(meal);
            return RedirectToAction(nameof(Interview1Edit), new { id = meal.InterviewId });
        }

        #endregion

        #region MealElement
        [HttpPost]
        public IActionResult MealElement3Create(MealCreateEdit viewModel)
        {
            Meal model = viewModel;
            // cache parent, then redirect
            _context.ItemUpdate<Meal>(model);
            // create new obj w/ reference to model
            var newMealElementId = _context.ItemAdd<MealElement>(new MealElement(model.Id));
            return RedirectToAction(nameof(MealElement3Edit), new { id = newMealElementId });
        }

        [HttpGet]
        public IActionResult MealElement3Edit(int id)
        {
            //var mealElementNewOrFromDb = _repoMealElement.ItemGetById(id);
            //MealElementCreateEdit mealElementCreateEdit = mealElementNewOrFromDb;
            //mealElementCreateEdit.Init(getNewNavigationBar());
            //return View(mealElementCreateEdit);
            return View(((MealElementCreateEdit)_context
                .ItemGetById<MealElement>(id)
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
                _context.ItemDelete<MealElement>(model);
            }
            else
            {
                model.IsCachedOnly = false;
                _context.ItemUpdate<MealElement>(model);
            }
            return RedirectToAction(nameof(Meal2Edit), new { id = model.MealId });
        }

        public IActionResult MealElement3Delete(int id)
        {
            var mealElement = _context.ItemGetById<MealElement>(id);
            _context.ItemDelete<MealElement>(mealElement);
            return RedirectToAction(nameof(Meal2Edit), new { id = mealElement.MealId });
        }
        public IActionResult MealElement3Details(int id) => Json(
            _context.ItemGetById<MealElement>(id)
            //_context.Set<MealElement>()
            //    .Include(i => i.Ingredient)
            //    .Include(i => i.IngredientGroup)
            //    .Include(i => i.Brandname)
            //    .ToList().FirstOrDefault(x => x.Id == id)
            );


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
