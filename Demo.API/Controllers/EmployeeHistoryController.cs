
using Demo.BusinessLayer.EmployeeHistory;
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
    public class EmployeeHistoryController : ControllerBase
    {
        private readonly IEmployeeHistoryService _iEmployeeHistoryService;

        public EmployeeHistoryController(IEmployeeHistoryService iEmployeeHistoryService)
        {
            _iEmployeeHistoryService = iEmployeeHistoryService;
        }

        [HttpGet]
        [Route("api/GetEmployeeEmployeeHistory/{employeeId}")]
        public async Task<IEnumerable<EmployeeHistoryViewmodel>> GetEmpEmployeeHistory(int employeeId)
        {
            return await _iEmployeeHistoryService.GetEmployeeEmployeeHistory(employeeId);
        }

        [HttpGet]
        [Route("api/GetEmployeeHistoryByEmployeeHistoryId/{id}")]
        public async Task<EmployeeHistoryViewmodel> GetEmployeeHistoryByEmployeeHistoryId(int id)
        {
            return await _iEmployeeHistoryService.GetEmployeeHistoryByEmployeeHistoryId(id);
        }

        [HttpGet]
        [Route("api/GetEmployeeHistory/{employeeId}")]
        public async Task<IEnumerable<EmployeeHistoryViewmodel>> GetEmployeeHistory(int employeeId)
        {
            return await _iEmployeeHistoryService.GetEmployeeHistory(employeeId);
        }


        [HttpPost]
        [Route("api/InsertEmployeePosition/{id}")]
        public async Task InsertEmployeePosition(EmployeeHistoryViewmodel employeeHistoryViewmodel, int id)
        {
           await _iEmployeeHistoryService.InsertEmployeePosition(employeeHistoryViewmodel, id);
        }


        [HttpPut]
        [Route("api/UpdateEmployeeEmployeeHistory/{employeeJobHistoryId}")]
        public async Task UpdateEmployeeEmployeeHistory(EmployeeHistoryViewmodel employeeHistoryViewmodel)
        {
             await _iEmployeeHistoryService.UpdateEmployeeEmployeeHistory(employeeHistoryViewmodel);
        }
    }
}
