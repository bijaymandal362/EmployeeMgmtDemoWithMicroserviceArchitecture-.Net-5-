using Demo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.BusinessLayer.Position
{
    public interface IPositionService 
    {

        Task InsertPosition(PositionViewModel positionViewModel);
        Task<IEnumerable<Entities.Entities.Position>> GetPosition();
    }
}
