using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Dal;
using NorbitsChallenge.Helpers;
using NorbitsChallenge.Models;

namespace NorbitsChallenge.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;

        public HomeController(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult Index()
        {
            var model = GetCompanyModel();
            return View(model);
        }

        [HttpPost]
        public JsonResult Index(int companyId, string licensePlate)
        {
            var CarDb = new CarDb(_config);
           
            var model = GetCompanyModel();

            return Json(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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

        private HomeModel GetCompanyModel()
        {
            var companyId = UserHelper.GetLoggedOnUserCompanyId();
            var companyName = new SettingsDb(_config).GetCompanyName(companyId);
            var companyCars = new CarDb(_config).GetAllCarsById(companyId);
            return new HomeModel { CompanyId = companyId, CompanyName = companyName, CompanyCars = companyCars };
        }

        public IActionResult AddCars()
        {
            return View();
        }

        public IActionResult AddCarsAction(Car car)
        {
            var carDb = new CarDb(_config);
            var model = GetCompanyModel();
            carDb.AddCar(car);
            return View("Index", model);
        }

        public IActionResult DeleteCar(string LicensePlate)
        {
            Car car = new CarDb(_config).GetSpecificCar(LicensePlate);
            return View(car);
        }

        [HttpPost]
        public IActionResult DeleteCarAction(string LicensePlate)
        {
            var carDb = new CarDb(_config);
            carDb.DeleteCar(LicensePlate);
            var model = GetCompanyModel();
            return View("Index", model);
        }

        public IActionResult AllCars()
        {
            var carDb = new CarDb(_config).GetAllCars();
            var model = GetCompanyModel();
            return View(carDb);
        }

        public IActionResult SearchForCar()
        {
            ViewData["Message"] = "Search for car";
            return View();
        }

        [HttpPost]
        public IActionResult SearchForCarAction([FromForm] string LicensePlate)
        {
            var carDb = new CarDb(_config).GetSpecificCar(LicensePlate);
            return View("SearchForCar", carDb);
        }

        public IActionResult EditCar(string LicensePlate)
        {
            Car car = new CarDb(_config).GetSpecificCar(LicensePlate);
            return View(car);
        }

        [HttpPost]

        public IActionResult EditCarAction(Car car)
        {
            var carDb = new CarDb(_config);
            carDb.UpdateCar(car);
            var model = GetCompanyModel();
            return View("Index", model);
        }
    }
}
