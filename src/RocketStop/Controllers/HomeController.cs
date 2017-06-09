using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RocketStop.Models;
using RocketStop.Options;

namespace RocketStop.Controllers
{
    public class HomeController : Controller
    {
        private readonly Config _config;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IOptions<Config> options, ILogger<HomeController> logger)
        {
            _config = options.Value;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation(1001, "GET /");
            ViewBag.Name = _config.RocketStopName;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
