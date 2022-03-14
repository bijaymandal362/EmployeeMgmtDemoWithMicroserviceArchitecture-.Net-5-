using Demo.BusinessLayer.Employees;
using Demo.Models;
using Demo.Models.ViewModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.API.Controllers
{
    [EnableCors("AllowAll")]
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeesService _iEmployeeService;


        public EmployeeController(IEmployeesService iEmployeeService)
        {
            _iEmployeeService = iEmployeeService;

        }

        [HttpGet]
        [Route("api/GetEmployee")]
        public async Task<IEnumerable<EmployeeViewModel>> GetEmployee()
        {
            return await _iEmployeeService.GetEmployee();
        }

        [HttpGet]
        [Route("api/GetEmployeeByEmployeeId/{employeeId}")]
        public async Task<IActionResult> GetEmployeeByEmployeeId(int employeeId)
        {
            var result = await _iEmployeeService.GetEmployeeByEmployeeId(employeeId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();

        }

        [HttpPost]
        [Route("api/InsertEmployeeTsk")]
        public void InsertEmployeeTsk(EmployeeViewModel employeeViewModel)
        {
            _iEmployeeService.InsertEmployeeTsk(employeeViewModel);

        }

        [HttpPut]
        [Route("api/UpdateEmployeeTsk/{employeeJobHistoryId}")]
        public async Task UpdateEmployeeTsk(EmployeeViewModel employeeViewModel)
        {
            await _iEmployeeService.UpdateEmployeeTsk(employeeViewModel);

        }

        [HttpDelete]
        [Route("api/DeleteEmployeeTsk/{employeeId}")]
        public async Task DeleteEmployeeTsk(int employeeId)
        {
            await _iEmployeeService.DeleteEmployeeTsk(employeeId);
        }
    }
}
