using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebOnline.Controllers
{
    public class BlogController : Controller
    {
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("blog")]
        public IActionResult ChiTiet()
        {
            return View();
        }
    }

}