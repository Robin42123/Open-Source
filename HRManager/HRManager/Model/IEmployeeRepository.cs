using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Model
{
	public interface IEmployeeRepository
	{
		Task<List<Employee>> GetAllAsync();
		Task AddAsync(Employee emp);
		Task UpdateAsync(Employee emp);
		Task DeleteAsync(int id);
	}
}
