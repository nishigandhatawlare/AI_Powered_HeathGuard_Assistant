﻿using Microsoft.AspNetCore.Mvc;

namespace Health_Guard_Assistant.Web.Controllers
{
    public class HealthMonitoringController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
