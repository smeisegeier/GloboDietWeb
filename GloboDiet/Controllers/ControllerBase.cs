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
    public class ControllerBase : Controller
    {
        // no readonly in base class!
        protected IWebHostEnvironment _webHostEnvironment;
        protected IRepository _repo;
        protected HttpContext _httpContext;

        public ControllerBase()
        {
        }
    }
}
