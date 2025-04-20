using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Model
{
	public class EmployeeService
	{
		private readonly IEmployeeRepository _repo;
		private readonly ILogger<EmployeeService> _logger;

		public EmployeeService(IEmployeeRepository repo, ILogger<EmployeeService> logger)
		{
			_repo = repo;
			_logger = logger;
		}

		public async Task ShowAllEmployeesAsync()
		{
			var list = await _repo.GetAllAsync();
			foreach (var emp in list)
				Console.WriteLine($"{emp.Id}: {emp.Name}, {emp.Role}, Age {emp.Age}");
		}

		public async Task AddEmployeeAsync()
		{
			Console.Write("Name: ");
			var name = Console.ReadLine();
			Console.Write("Role: ");
			var role = Console.ReadLine();
			Console.Write("Age: ");
			var age = int.Parse(Console.ReadLine() ?? "0");

			await _repo.AddAsync(new Employee { Name = name, Role = role, Age = age });
			_logger.LogInformation("Employee added: {Name}", name);
		}

		public async Task DeleteEmployeeAsync()
		{
			Console.Write("Enter ID to delete: ");
			var id = int.Parse(Console.ReadLine() ?? "0");
			await _repo.DeleteAsync(id);
			_logger.LogInformation("Employee deleted: {ID}", id);
		}
	}
}
