using GloboDiet.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Controllers
{
    public class ControllerBase : Controller
    {
        protected IWebHostEnvironment _webHostEnvironment;
        protected IRepository _repo;

        public ControllerBase()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
