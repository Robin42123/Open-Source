using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using HRManager.Model;

// Build configuration
var config = new ConfigurationBuilder()
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json", true, true)
	.Build();

// Setup DI
var services = new ServiceCollection();
services.AddSingleton<IConfiguration>(config);
services.AddDbContext<AppDbContext>(options =>
	options.UseSqlite(config.GetConnectionString("Default")));

services.AddLogging(builder => builder.AddConsole());
services.AddScoped<IEmployeeRepository, EmployeeRepository>();
services.AddScoped<EmployeeService>();
services.AddSingleton<AuthService>();

var provider = services.BuildServiceProvider();

// Apply DB migrations
var context = provider.GetRequiredService<AppDbContext>();
context.Database.EnsureCreated();

// Auth
var authService = provider.GetRequiredService<AuthService>();
if (!authService.Login())
{
	Console.WriteLine("Invalid credentials.");
	return;
}

// Main Menu
var empService = provider.GetRequiredService<EmployeeService>();

while (true)
{
	Console.WriteLine("\n1. View All\n2. Add\n3. Delete\n4. Exit");
	var choice = Console.ReadLine();

	switch (choice)
	{
		case "1": await empService.ShowAllEmployeesAsync(); break;
		case "2": await empService.AddEmployeeAsync(); break;
		case "3": await empService.DeleteEmployeeAsync(); break;
		case "4": return;
	}
}
