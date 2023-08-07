using System;
using EmployeePayslip.Models;

namespace EmployeePayslip.Data
{
    public interface IIncomeTaxManager
    {
        IEnumerable<TaxSlabModel> GetTaxSlabs();
    }

    public class IncomeTaxManager : IIncomeTaxManager
    {
        public IEnumerable<TaxSlabModel> GetTaxSlabs()
        {
            // This tax slab we can store in database on the basis of financial year
            return new SortedSet<TaxSlabModel>{
                new TaxSlabModel { UptoIncome = 14000, Rate = 0.105m },
                new TaxSlabModel { UptoIncome = 48000, Rate = 0.175m },
                new TaxSlabModel { UptoIncome = 70000, Rate = 0.30m },
                new TaxSlabModel { UptoIncome = 180000, Rate = 0.33m },
                new TaxSlabModel { UptoIncome = Int64.MaxValue, Rate = 0.39m }
            };
        }
    }
}

