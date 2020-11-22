using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GoHan.Controllers
{
    public class BaseController : Controller
    {
        public NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public virtual IActionResult Index()
        {
            return View();
        }
    }
}
