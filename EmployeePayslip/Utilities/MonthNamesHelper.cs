using System;
using EmployeePayslip.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeePayslip.Utilities
{
    public static class MonthNamesHelper
    {
        public static List<SelectListItem> GetMonthSelectList()
        {
            var monthSelectList = Enum.GetValues(typeof(MonthNamesEnum))
                                      .Cast<MonthNamesEnum>()
                                      .Select(month => new SelectListItem
                                      {
                                          Value = ((int)month).ToString(),
                                          Text = month.ToString()
                                      })
                                      .ToList();

            return monthSelectList;
        }
    }
}

