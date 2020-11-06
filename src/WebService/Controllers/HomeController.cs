﻿using System.Diagnostics;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebService.Models;

namespace WebService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;

        public HomeController(ILogger<HomeController> logger)
        {
            this.logger = logger;
        }

        public IActionResult Index()
        {
            var current = WindowsIdentity.GetCurrent();

            var message = $"Name: {current.Name.ToString()}";

            this.logger.LogInformation(message);

            User user = new User(current.Name);

            return View(user);
        }

        [HttpPost]
        public IActionResult Register()
        {
            return RedirectToAction(nameof(this.Index));
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
