using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoFinal.Models;
using MongoFinal.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace MongoFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicantsService _appSvc;

        public HomeController(ILogger<HomeController> logger, ApplicantsService applicantsService)
        {
            _logger = logger;
            _appSvc = applicantsService;
        }




        public IActionResult Index(int? ent)
        {
            if(ent.HasValue)
            {
                return View(_appSvc.Ent_Filter(ent));
            }
            return View(_appSvc.Read());
        }

        [HttpGet]
        public ActionResult Create() => View();

        [HttpGet]
        public ActionResult Statistics()
        {
            var st = _appSvc.Statistics();
            foreach (var i in st)
            {
                ViewData[i.GetValue("_id").ToString()] = i.GetValue("avgEnt");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            return View(_appSvc.Find(id));
        }

       /* [HttpGet]
        public ActionResult Ent_Filter(int ent)
        {
            return View("Index",_appSvc.Ent_Filter(ent));
        }*/


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult<Applicants> Create(Applicants applicants)
        {
            if (ModelState.IsValid)
            {
                _appSvc.Create(applicants);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult<Applicants> Edit(string id) =>
            View(_appSvc.Find(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Applicants applicants)
        {
            if (ModelState.IsValid)
            {
                _appSvc.Update(applicants);
                return RedirectToAction("Index");
            }
            return View(applicants);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            _appSvc.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
