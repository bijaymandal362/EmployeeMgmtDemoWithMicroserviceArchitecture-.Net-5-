using Demo.BusinessLayer.Position;
using Demo.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.API.Controllers
{
    [EnableCors("AllowAll")]
    [Route("[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _iPositionService;

        public PositionController(IPositionService iPositionService)
        {
          _iPositionService = iPositionService;
        }

        [HttpGet]
        [Route("api/GetPosition")]
        public async Task<IEnumerable<Entities.Entities.Position>> GetPosition()
        {
           return await _iPositionService.GetPosition();
        }

        [HttpPost]
        [Route("api/InsertPosition")]
        public async Task InsertPosition(PositionViewModel positionViewModel)
        {
          await _iPositionService.InsertPosition(positionViewModel);
        }

       

       
    }
}
