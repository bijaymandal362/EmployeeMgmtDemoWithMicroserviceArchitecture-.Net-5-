
using Demo.Models.ViewModel;


using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.BusinessLayer.Employees
{
    public interface IEmployeesService
    {
        Task<IEnumerable<EmployeeViewModel>> GetEmployee();
        Task<EmployeeViewModel> GetEmployeeByEmployeeId(int? employeeId);

        Task InsertEmployeeTsk(EmployeeViewModel employeeViewModel);

        Task UpdateEmployeeTsk(EmployeeViewModel employeeViewModel);
        Task DeleteEmployeeTsk(int employeeyId);



    }
}
