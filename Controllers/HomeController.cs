using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text.Json;
using lr2_Company.Models;

namespace lr2_Company.Controllers
{
    public class HomeController : Controller
    {
        private readonly CompanyService _companyService;

        public HomeController(CompanyService companyService)
        {
            _companyService = companyService;
        }

        public IActionResult Index()
        {
            var topCompany = _companyService.GetCompanyWithMostEmployees();
            ViewBag.TopCompany = topCompany;
            return View();
        }

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
            return View();
        }
    }
}
