using EmployeePayslip.Data;
using EmployeePayslip.Services;
using EmployeePayslip.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add servicISes to the container.
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IMemoryCacheWrapper, MemoryCacheWrapper>();
builder.Services.AddScoped<IEmployeePayslipService, EmployeePayslipService>();
builder.Services.AddScoped<IIncomeTaxService, IncomeTaxService>();
builder.Services.AddScoped<IEmployeePayslipManager, EmployeePayslipManager>();
builder.Services.AddScoped<IIncomeTaxManager, IncomeTaxManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();

public partial class Program { }
