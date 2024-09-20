using lr2_Comp.Controllers;
using lr2_Company.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;

namespace lr2_Company.Controllers
{
    public class HomeController : Controller
    {
        private readonly CompanyService _companyService;

        public HomeController()
        {
            _companyService = new CompanyService();
        }


        public IActionResult Index()
        {
            var topCompany = _companyService.GetCompanyWithMostEmployees();
            ViewBag.TopCompany = topCompany;
            return View();
        }

        // Метод для виведення інформації з JSON
        public IActionResult MyInfo()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MyName.json");
            Person personData = null;

            if (System.IO.File.Exists(filePath))
            {
                var json = System.IO.File.ReadAllText(filePath);
                personData = JsonSerializer.Deserialize<Person>(json);
            }

            ViewBag.Person = personData;
            return View("MyInfo"); 
        }
    }
}