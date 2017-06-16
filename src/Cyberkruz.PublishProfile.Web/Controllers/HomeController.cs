using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Cyberkruz.PublishProfile.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cyberkruz.PublishProfile.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
