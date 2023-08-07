using System;
using EmployeePayslip.Data;
using EmployeePayslip.Enums;
using EmployeePayslip.Models;

namespace EmployeePayslip.Services
{
    public interface IEmployeePayslipService
    {
        IEnumerable<EmployeePayslipResponse> GetEmployeePayslipData();
        IEnumerable<EmployeePayslipResponse> PostEmployeePayslipData(EmployeePayslipModel employeePayslipModel);
    }

    public class EmployeePayslipService : IEmployeePayslipService
    {
        private readonly IEmployeePayslipManager _employeePayslipManager;
        private readonly IIncomeTaxService _incomeTaxService;
        private readonly IConfiguration _configuration;

        public EmployeePayslipService(IEmployeePayslipManager employeePayslipManager,
            IIncomeTaxService incomeTaxService,
            IConfiguration configuration)
        {
            _employeePayslipManager = employeePayslipManager;
            _incomeTaxService = incomeTaxService;
            _configuration = configuration;
        }

        public IEnumerable<EmployeePayslipResponse> GetEmployeePayslipData()
        {
            string cacheKey = _configuration["EmployeePayslipDataCacheKey"] ?? "EmployeePayslipDataCacheKey";
            return _employeePayslipManager.GetData(cacheKey);
        }

        public IEnumerable<EmployeePayslipResponse> PostEmployeePayslipData(EmployeePayslipModel employeePayslipModel)
        {
            string cacheKey = _configuration["EmployeePayslipDataCacheKey"] ?? "EmployeePayslipDataCacheKey";

            var employeePayslipResponse = GetEmployeePayslipResponse(employeePayslipModel);

            return _employeePayslipManager.SetData(cacheKey, employeePayslipResponse);
        }

        private static string GetPayPeriod(int month)
        {
            // Get the first date of the specified month and year
            DateTime firstDateOfMonth = new DateTime(DateTime.UtcNow.Year, month, 1);

            // Get the last date of the specified month and year
            DateTime lastDateOfMonth = firstDateOfMonth.AddMonths(1).AddDays(-1);

            return $"{firstDateOfMonth.Day} {Enum.GetName(typeof(MonthNamesEnum), firstDateOfMonth.Month)} - " +
                   $"{lastDateOfMonth.Day} {Enum.GetName(typeof(MonthNamesEnum), lastDateOfMonth.Month)}";
        }

        private EmployeePayslipResponse GetEmployeePayslipResponse(EmployeePayslipModel employeePayslipModel)
        {
            decimal grossIncome = (decimal)employeePayslipModel.AnnualSalary / 12;
            decimal incomeTax = _incomeTaxService.GetIncomeTax(employeePayslipModel.AnnualSalary);

            return new EmployeePayslipResponse
            {
                Name = $"{employeePayslipModel.FirstName} {employeePayslipModel.LastName}",
                PayPeriod = GetPayPeriod(employeePayslipModel.Period),
                GrossIncome = Math.Round(grossIncome, 2),
                IncomeTax = Math.Round(incomeTax, 2),
                NetIncome = Math.Round(grossIncome, 2) - Math.Round(incomeTax, 2),
                Super = Math.Round((grossIncome * (decimal)employeePayslipModel.SuperRate) / 100, 2)
            };
        }
    }
}
