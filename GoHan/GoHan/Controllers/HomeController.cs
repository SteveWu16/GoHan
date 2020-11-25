using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GoHan.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System;
using System.Linq;

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
            var content = "請登入啦";

            ViewBag.Content = content;

            return View();
        }

        public IActionResult RegisterPage()
        {
            var content = "請註冊啦";

            ViewBag.Content = content;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Register(UserInfoModel request)
        {
            if (HasSpecialChars(request.Username) || HasSpecialChars(request.Password))
            {
                ViewBag.Content = "Invalid input, please don't type shit";
                return View();
            }
            try
            {
                var connString = "Server=127.0.0.1; Port=3306;User Id=root;Password=No5teven_HMW;Database=gohandb;charset=utf8;";
                var conn = new MySqlConnection
                {
                    ConnectionString = connString
                };

                if (conn.State != ConnectionState.Open)
                    conn.Open();

                string sql = $@"INSERT INTO `userinfo` (`username`, `password`) VALUES
                           ('{request.Username}', '{request.Password}')";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                var index = cmd.ExecuteNonQuery();

                var success = false;
                if (index > 0)
                {
                    ViewBag.Content = $"Create success on user {request.Username}";
                    success = true;
                }

                else
                {
                    ViewBag.Content = $"Create fail on user {request.Username}";
                    success = false;
                }
                
                return View();
            }
            catch (Exception e)
            {
                logger.Error($"Error when create player => {e}");

                if (e.ToString().ToLower().Contains("primary"))
                {
                    ViewBag.Content = "Create player fail: Duplicate player";
                    return View();
                }

                ViewBag.Content = "Create player fail: Unkown error";
                return View();

            }

            
        }

        [HttpPost]
        public IActionResult Login(UserInfoModel request)
        {
            if (HasSpecialChars(request.Username) || HasSpecialChars(request.Password))
            {
                ViewBag.Content = "Invalid input, please don't type shit";
                return View();
            }

            foreach (var i in _dbContext.UserInfo)
            {
                if (request.Username == i.Username && request.Password == i.Password)
                {
                    ViewBag.Content = $"Welcome! {i.Username}";
                    return View();
                }
            }

            ViewBag.Content = "Login fail";
            return View();
        }

        private bool HasSpecialChars(string yourString)
        {
            return yourString.Any(ch => !char.IsLetterOrDigit(ch));
        }

    }
}
