using lr2_Company.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml.Linq;

namespace lr2_Comp.Controllers
{
    public class CompanyService
    {
        private readonly string _configPath;

        public CompanyService()
        {
            _configPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }

        public Company GetCompanyWithMostEmployees()
        {
            var companies = new List<Company>();

            var xmlFilePath = Path.Combine(_configPath, "companies.xml");
            var jsonFilePath = Path.Combine(_configPath, "companies.json");
            var iniFilePath = Path.Combine(_configPath, "companies.ini");

            if (File.Exists(xmlFilePath))
            {
                companies.AddRange(ReadCompaniesFromXml(xmlFilePath));
            }

            if (File.Exists(jsonFilePath))
            {
                companies.AddRange(ReadCompaniesFromJson(jsonFilePath));
            }

            if (File.Exists(iniFilePath))
            {
                companies.AddRange(ReadCompaniesFromIni(iniFilePath));
            }

            // Знайти компанію з найбільшою кількістю співробітників
            return companies.OrderByDescending(c => c.Employees).FirstOrDefault();
        }

        private List<Company> ReadCompaniesFromXml(string filePath)
        {
            var companies = new List<Company>();
            var doc = XDocument.Load(filePath);

            foreach (var element in doc.Descendants("company"))
            {
                companies.Add(new Company
                {
                    Name = element.Element("name")?.Value,
                    Employees = int.Parse(element.Element("employees")?.Value)
                });
            }

            return companies;
        }

        private List<Company> ReadCompaniesFromJson(string filePath)
        {
            var companies = new List<Company>();
            var json = File.ReadAllText(filePath);
            var jsonDoc = JsonDocument.Parse(json);

            foreach (var element in jsonDoc.RootElement.GetProperty("companies").EnumerateArray())
            {
                companies.Add(new Company
                {
                    Name = element.GetProperty("name").GetString(),
                    Employees = element.GetProperty("employees").GetInt32()
                });
            }

            return companies;
        }

        private List<Company> ReadCompaniesFromIni(string filePath)
        {
            var companies = new List<Company>();
            var lines = File.ReadAllLines(filePath);
            string currentCompany = null;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    currentCompany = line.Trim('[', ']');
                }
                else if (currentCompany != null)
                {
                    var keyValue = line.Split('=');
                    if (keyValue.Length == 2 && keyValue[0].Trim() == "employees")
                    {
                        companies.Add(new Company
                        {
                            Name = currentCompany,
                            Employees = int.Parse(keyValue[1].Trim())
                        });
                    }
                }
            }

            return companies;
        }
    }
}
