using System.Collections.Generic;

namespace lr2_Company.Models
{
    public class CompanyService
    {
        private readonly List<Company> _companies;

        public CompanyService(List<Company> companies)
        {
            _companies = companies;
        }

        public Company GetCompanyWithMostEmployees()
        {
            Company largestCompany = null;
            int maxEmployees = 0;

            foreach (var company in _companies)
            {
                if (company.Employees > maxEmployees)
                {
                    maxEmployees = company.Employees;
                    largestCompany = company;
                }
            }

            return largestCompany;
        }
    }
}
