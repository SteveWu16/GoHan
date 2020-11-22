using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GoHan.Models;
using MySql.Data.MySqlClient;
using System.Data;
using System;

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


            foreach (var c in _dbContext.DBInit)
            {
                content += $"id = {c.id}, DateTime = {c.Date}, Message = {c.Message}";
            }

            logger.Info($"{content}");
            ViewBag.Content = content;

            ViewBag.RegisterResult = Register("Steven1", "test1234");

            return View();
        }

        public IActionResult Privacy()
        {
            // Establish DB connection
            var connString = "Server=127.0.0.1; Port=3306;User Id=root;Password=No5teven_HMW;Database=gohandb;charset=utf8;";
            var conn = new MySqlConnection
            {
                ConnectionString = connString
            };

            if (conn.State != ConnectionState.Open)
                conn.Open();

            string sql = @"INSERT INTO `test` (`date`, `message`) VALUES
                           ('2020-11-21 01:00:00', 'GoHan1'),
                           ('2020-11-21 02:00:00', 'GoHan1')";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            int index = cmd.ExecuteNonQuery();
            var success = false;
            if (index > 0)
                success = true;
            else
                success = false;


            var content = "";
            foreach (var c in _dbContext.DBInit)
            {
                content += $"id = {c.id}, DateTime = {c.Date}, Message = {c.Message}";
            }

            ViewBag.Content = $"Write data is {success}, the content now includes {content}";


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public string Register(string username, string password)
        {
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
                           ('{username}', '{password}')";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                var index = cmd.ExecuteNonQuery();

                var success = false;
                if (index > 0)
                    success = true;
                else
                    success = false;
            }
            catch (Exception e)
            {
                logger.Error($"Error when create player => {e}");

                if (e.ToString().Contains("Primary"))
                {
                    return "Create player fail: Duplicate player";
                }
                return "Create player fail: Unkown error";

            }

            return $"Create success on user {username}";
        }

    }
}
