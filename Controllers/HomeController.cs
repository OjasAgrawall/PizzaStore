using System.Diagnostics;
using LearningEntityFramework.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningEntityFramework.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
