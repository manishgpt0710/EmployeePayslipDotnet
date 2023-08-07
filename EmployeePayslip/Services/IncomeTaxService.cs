using System;
using EmployeePayslip.Data;

namespace EmployeePayslip.Services
{
    public interface IIncomeTaxService
    {
        decimal GetIncomeTax(long annualSalary);
    }

    public class IncomeTaxService : IIncomeTaxService
    {
        private readonly IIncomeTaxManager _incomeTaxManager;

        public IncomeTaxService(IIncomeTaxManager incomeTaxManager)
        {
            _incomeTaxManager = incomeTaxManager;
        }

        public decimal GetIncomeTax(long annualSalary)
        {
            if (annualSalary <= 0) return 0;

            decimal incomeTax = 0;
            var taxSlabs = _incomeTaxManager.GetTaxSlabs().ToList();
            taxSlabs.Insert(0, new Models.TaxSlabModel{ UptoIncome = 0, Rate = 0 });

            for (int i=1; i<taxSlabs.Count; i++)
            {
                if (annualSalary <= (taxSlabs[i].UptoIncome - taxSlabs[i-1].UptoIncome))
                {
                    incomeTax += annualSalary * taxSlabs[i].Rate;
                    // Calculate income tax on monthly basis
                    return incomeTax / 12;
                }
                else
                {
                    incomeTax += (taxSlabs[i].UptoIncome - taxSlabs[i-1].UptoIncome) * taxSlabs[i].Rate;
                    annualSalary -= taxSlabs[i].UptoIncome - taxSlabs[i-1].UptoIncome;
                }
            }

            // Calculate income tax on monthly basis
            return incomeTax / 12;
        }
    }
}
