using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GoHan.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace GoHan.Controllers
{
    public class HomeController : BaseController
    {
        private readonly DBContext _dbContext;

        public HomeController(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override IActionResult Index()
        {
            var content = "";

            logger.Debug("Debug");
            logger.Info($"{_dbContext.DBInit}");

            foreach(var c in _dbContext.DBInit)
            {
                content += $"id = {c.id}, DateTime = {c.Date}, Message = {c.Message}";
            }

            ViewBag.Content = content;
            return View();
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
