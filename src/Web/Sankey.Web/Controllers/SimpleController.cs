using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class SimpleController : Controller
    {
        public IActionResult Index(string geography="ca", int year=2016)
        {
            ViewData["Message"] = "Your application description page.";

            ViewBag.Geography = geography;
            ViewBag.Year = year;

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
