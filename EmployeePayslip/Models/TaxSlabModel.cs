using System;
using System.Collections.Generic;
namespace EmployeePayslip.Models
{
    public class TaxSlabModel : IComparable<TaxSlabModel>
    {
        public long UptoIncome { get; set; }
        public decimal Rate { get; set; }

        public int CompareTo(TaxSlabModel other)
        {   
            // Implement comparison logic based on UptoTaxableIncome
            return UptoIncome.CompareTo(other.UptoIncome);
        }
    }
}
