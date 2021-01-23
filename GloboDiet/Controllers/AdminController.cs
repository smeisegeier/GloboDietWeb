using GloboDiet.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Controllers
{
    public class AdminController : ControllerBase
    {
        public AdminController(IWebHostEnvironment webHostEnvironment, IRepository repo, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _repo = repo;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public IActionResult Index()
        {
            return Content(_httpContext.Session.GetString("SessionUser"));
        }
    }
}
