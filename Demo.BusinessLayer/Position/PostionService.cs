using Demo.Entities.Data;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.BusinessLayer.Position
{
    public class PostionService : IPositionService
    {
        private readonly DemoDbContext _context;

        public PostionService(DemoDbContext context, IToastNotification toastNotification)
        {
            _context = context;
        }

        public async Task InsertPosition(PositionViewModel positionViewModel)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Demo.Entities.Entities.Position position = new Entities.Entities.Position();

                    if (position.PositionId == 0)
                    {
                        position.PositionName = positionViewModel.PositionName;
                        await _context.Position.AddAsync(position);
                        await _context.SaveChangesAsync();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {

                    transaction.Rollback();
                }
            }

        }

        public async Task<IEnumerable<Entities.Entities.Position>> GetPosition()
        {
            List<Demo.Entities.Entities.Position> list = new List<Entities.Entities.Position>();
            list = await _context.Position.ToListAsync();
            list.Insert(0, new Demo.Entities.Entities.Position { PositionId = 0, PositionName = "Please Select" });
            return list;
        }


    }

}
