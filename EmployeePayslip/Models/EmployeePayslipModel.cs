using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeePayslip.Models
{
    public class EmployeePayslipModel
    {
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Range(minimum: 1, maximum: long.MaxValue, ErrorMessage = "The Annual Salary field is required.")]
        [Display(Name = "Annual Salary")]
        public long AnnualSalary { get; set; }

        [Range(minimum: 0 , maximum: 50, ErrorMessage = "The Super Rate field is required.")]
        [Display(Name = "Super Rate")]
        public int SuperRate { get; set; }

        [Display(Name = "Pay Period")]
        public int Period { get; set; }
    }
}
