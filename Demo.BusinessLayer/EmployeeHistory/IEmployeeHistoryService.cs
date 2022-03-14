using Demo.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLayer.EmployeeHistory
{
    public interface IEmployeeHistoryService
    {
        Task<IEnumerable<EmployeeHistoryViewmodel>> GetEmployeeHistory(int employeeId);
        Task<IEnumerable<EmployeeHistoryViewmodel>> GetEmployeeEmployeeHistory(int? employeeId);
        Task<EmployeeHistoryViewmodel> GetEmployeeHistoryByEmployeeHistoryId(int employeeHistoryId);
        Task UpdateEmployeeEmployeeHistory(EmployeeHistoryViewmodel employeeHistoryViewmodel);

        Task InsertEmployeePosition(EmployeeHistoryViewmodel employeeHistoryViewmodel, int? id);


    }
}
