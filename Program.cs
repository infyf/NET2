using lr2_Company.Models;

var builder = WebApplication.CreateBuilder(args);

// Підключення конфігураційних файлів
builder.Configuration.AddJsonFile("wwwroot/companies.json", optional: true, reloadOnChange: true);
builder.Configuration.AddXmlFile("wwwroot/companies.xml", optional: true, reloadOnChange: true);
builder.Configuration.AddIniFile("wwwroot/companies.ini", optional: true, reloadOnChange: true);

// Метод для зчитування компанії з конфігурації
List<Company> GetCompaniesFromConfig(IConfiguration config)
{
    var companies = new List<Company>();

    for (int i = 0; i < 3; i++) // Припускаємо, що компаній у файлі 3
    {
        var name = config[$"companies:{i}:name"];
        var employeeCountStr = config[$"companies:{i}:employees"];

        if (!string.IsNullOrEmpty(name) && int.TryParse(employeeCountStr, out int employeeCount))
        {
            companies.Add(new Company { Name = name, Employees = employeeCount });
        }
    }

    return companies;
}

// Зчитування компаній
var companies = GetCompaniesFromConfig(builder.Configuration);

builder.Configuration.AddJsonFile("wwwroot/MyName.json", optional: true, reloadOnChange: true);

// Додавання сервісу
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton(new CompanyService(companies));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
