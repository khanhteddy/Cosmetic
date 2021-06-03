using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cosmetic.Models;

namespace Cosmetic.Controllers
{
    public class AjaxController : Controller
    {
        private MyPhamContext db;
        public AjaxController(MyPhamContext ctx)
        {
            db = ctx;
        }
        public IActionResult ServerTime()
        {
            return Content(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        }
        public IActionResult Index()
        {
            return View();
        }

    }
}