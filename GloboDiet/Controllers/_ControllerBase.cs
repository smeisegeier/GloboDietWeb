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
using System.Net;


namespace GloboDiet.Controllers
{
    /// <summary>
    /// This provides a superclass for all controller.
    /// Pros: UI methods like getNavigationBar is now common for all controllers
    /// Cons: to prevent a very unflexible ctor, protected props are initialized from subclasses. Thus, they cant longer be readonly.
    /// </summary>
    public abstract class _ControllerBase : Controller
    {

        protected IWebHostEnvironment _webHostEnvironment;
        protected static NLog.Logger _nLogger = NLog.LogManager.GetCurrentClassLogger();
        protected HttpContext _httpContext;
        protected LookupData _lookupData;

        // example for domain repo
        protected IRepositoryNew<Interview> _repoInterview;
        protected IRepositoryNew<Interviewer> _repoInterviewer;
        protected IRepositoryNew<Location> _repoLocation;
        protected IRepositoryNew<Respondent> _repoRespondent;
        protected IRepositoryNew<Meal> _repoMeal;
        protected IRepositoryNew<MealElement> _repoMealElement;
        
        public _ControllerBase()
        {
        }

        protected NavigationBar getNewNavigationBar() => new NavigationBar(_repoInterview.ItemsGetCount(), _repoInterviewer.ItemsGetCount(), _repoLocation.ItemsGetCount(), _repoRespondent.ItemsGetCount(), _repoInterview.GetSqlConnectionType());

    }
}
