using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Model
{
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly AppDbContext _context;

		public EmployeeRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<List<Employee>> GetAllAsync() => await _context.Employees.ToListAsync();

		public async Task AddAsync(Employee emp)
		{
			_context.Employees.Add(emp);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Employee emp)
		{
			_context.Employees.Update(emp);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id)
		{
			var emp = await _context.Employees.FindAsync(id);
			if (emp != null)
			{
				_context.Employees.Remove(emp);
				await _context.SaveChangesAsync();
			}
		}
	}
}
