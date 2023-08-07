using EmployeePayslip.Models;
using EmployeePayslip.Services;
using EmployeePayslip.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeePayslip.Pages;

public class IndexModel : PageModel
{
    private readonly IEmployeePayslipService _employeePayslipService;
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(IEmployeePayslipService employeePayslipService,
        ILogger<IndexModel> logger)
    {
        _employeePayslipService = employeePayslipService;
        _logger = logger;
    }

    [BindProperty]
    public EmployeePayslipModel Input { get; set; }

    public static List<EmployeePayslipResponse> Output { get; set; }

    public static List<SelectListItem> PeriodMonths { get; set; }

    public void OnGet()
    {
        // Populate the month names from the MonthNames Enum
        PeriodMonths = MonthNamesHelper.GetMonthSelectList();
        PeriodMonths[0].Selected = true;

        Output = _employeePayslipService.GetEmployeePayslipData().ToList();
    }

    public IActionResult OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Output = _employeePayslipService.PostEmployeePayslipData(Input).ToList();

        return RedirectToPage("Index", new { Input = new EmployeePayslipModel() });
    }
}
