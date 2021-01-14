using GloboDiet.Data;
using GloboDiet.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace GloboDiet.Controllers
{

    public class InterviewerController : ControllerBase
    {

        public InterviewerController(IWebHostEnvironment webHostEnvironment, IRepository repo) 
        {
            _webHostEnvironment = webHostEnvironment;
            _repo = repo;
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(new Interviewer());
        }

        [HttpPost]
        public IActionResult Create(Interviewer interviewer, string submit)
        {

            _repo.AddInterviewer(interviewer);
            return Redirect("~/Home/Index");
        }

        public IActionResult List()
        {
            var list = _repo.GetAllInterviewers();
            return View(list);
        }

        [HttpGet]
        public IActionResult CreateLocation()
        {
            return View(new Location());
        }

        [HttpPost]
        public IActionResult Createlocation(Location location)
        {
            _repo.AddLocation(location);
            return Redirect("~/Home/Index");
        }
        public IActionResult ListLocations()
        {
            var list = _repo.GetAllLocations();
            return View(list);
        }
    }
}
