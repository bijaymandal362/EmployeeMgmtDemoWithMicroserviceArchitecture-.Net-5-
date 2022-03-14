using Demo.Entities.Data;
using Demo.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.BusinessLayer.EmployeeHistory
{
    public class EmployeeHistoryService : IEmployeeHistoryService
    {
        private readonly DemoDbContext _context;
        private readonly IToastNotification _toastNotification;

        public EmployeeHistoryService(DemoDbContext context, IToastNotification toastNotification)
        {
            _context = context;   
        }
        public async Task<EmployeeHistoryViewmodel> GetEmployeeHistoryByEmployeeHistoryId(int id)
        {
            var lst = await (from e in _context.EmployeeHistory
                             join eh in _context.Employee on e.EmployeeId equals eh.EmployeeId
                             join p in _context.Position on e.PositionId equals p.PositionId
                             where e.EmployeeJobHistoryId == id

                             select new EmployeeHistoryViewmodel
                             {
                                 EmployeeJobHistoryId = e.EmployeeJobHistoryId,
                                 EmployeeId = e.EmployeeId,
                                 PositionId = p.PositionId,
                                 PositionName = p.PositionName,
                                 StartDate = e.StartDate,
                                 EndDate = e.EndDate
                             }).FirstOrDefaultAsync();


            return lst;
        }

        public async Task<IEnumerable<EmployeeHistoryViewmodel>> GetEmployeeHistory(int employeeId)
        {

            try
            {
                var lst = await (from eh in _context.EmployeeHistory
                                 join e in _context.Employee on eh.EmployeeId equals e.EmployeeId
                                 join p in _context.Position on eh.PositionId equals p.PositionId
                                 where eh.EmployeeId == employeeId

                                 select new EmployeeHistoryViewmodel
                                 {
                                     EmployeeJobHistoryId = eh.EmployeeJobHistoryId,
                                     EmployeeId = eh.EmployeeId,
                                     PositionName = p.PositionName,
                                     StartDate = eh.StartDate,
                                     EndDate = eh.EndDate
                                 }).ToListAsync();
                return lst;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<IEnumerable<EmployeeHistoryViewmodel>> GetEmployeeEmployeeHistory(int? employeeId)
        {

            try
            {
                var lst = await (from eh in _context.EmployeeHistory
                                 join e in _context.Employee on eh.EmployeeId equals e.EmployeeId
                                 join p in _context.Position on eh.PositionId equals p.PositionId
                                 where eh.EmployeeId == employeeId

                                 select new EmployeeHistoryViewmodel
                                 {
                                     EmployeeJobHistoryId = eh.EmployeeJobHistoryId,
                                     EmployeeId = eh.EmployeeId,
                                     PositionName = p.PositionName,
                                     StartDate = eh.StartDate,
                                     EndDate = eh.EndDate

                                 }).ToListAsync();
                return lst;
            }
            catch (Exception ex)
            {
                throw;
            }

        }


        public async Task UpdateEmployeeEmployeeHistory(EmployeeHistoryViewmodel employeeHistoryViewmodel)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {

                try
                {
                    var updateEmloyeeEmployeeHistory = await (from eh in _context.EmployeeHistory
                                                              join e in _context.Employee on eh.EmployeeId equals e.EmployeeId
                                                              where eh.EmployeeJobHistoryId == employeeHistoryViewmodel.EmployeeJobHistoryId && eh.EmployeeId == employeeHistoryViewmodel.EmployeeId
                                                              select new EmployeeHistoryViewmodel
                                                              {
                                                                  EmployeeId = e.EmployeeId,
                                                                  PositionId = eh.PositionId,
                                                                  StartDate = eh.StartDate,
                                                                  EndDate = eh.EndDate
                                                              }).FirstOrDefaultAsync();

                    if (updateEmloyeeEmployeeHistory != null)
                    {
                        DateTime st = Convert.ToDateTime(employeeHistoryViewmodel.StartDate);
                        DateTime et = Convert.ToDateTime(employeeHistoryViewmodel.EndDate);
                        if (st <= et)
                        {
                            updateEmloyeeEmployeeHistory.EmployeeId = employeeHistoryViewmodel.EmployeeId;
                            updateEmloyeeEmployeeHistory.PositionId = employeeHistoryViewmodel.PositionId;
                            updateEmloyeeEmployeeHistory.StartDate = employeeHistoryViewmodel.StartDate;
                            updateEmloyeeEmployeeHistory.EndDate = employeeHistoryViewmodel.EndDate;

                            await _context.SaveChangesAsync();
                            transaction.Commit();
                        }

                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }

            }
        }

        public async Task InsertEmployeePosition(EmployeeHistoryViewmodel employeeHistoryViewmodel, int? id)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Demo.Entities.Entities.EmployeeHistory employeeHistory = new Entities.Entities.EmployeeHistory();
                    Demo.Entities.Entities.Employee employee = new Entities.Entities.Employee();

                    var date = await _context.EmployeeHistory.Include("Employee").Where(a => a.EmployeeId == id).FirstOrDefaultAsync();

                    if (date.EndDate < employeeHistoryViewmodel.StartDate)
                    {
                        DateTime st = Convert.ToDateTime(employeeHistoryViewmodel.StartDate);
                        DateTime et = Convert.ToDateTime(employeeHistoryViewmodel.EndDate);
                        if (st <= et)
                        {
                            var empPosition = _context.Employee.FirstOrDefault(a => a.EmployeeId == id);
                            employeeHistory.EmployeeId = (int)id;
                            empPosition.PositionId = employeeHistoryViewmodel.PositionId;
                            employeeHistory.PositionId = (int)employeeHistoryViewmodel.PositionId;
                            employeeHistory.StartDate = employeeHistoryViewmodel.StartDate;
                            employeeHistory.EndDate = employeeHistoryViewmodel.EndDate;
                            await _context.EmployeeHistory.AddAsync(employeeHistory);
                            await _context.SaveChangesAsync();
                            transaction.Commit();
                            
                        }
                        else
                        {
                            throw new Exception();
                           
                        }
                    }
                    else
                    {
                        throw new Exception();

                    }


                }
                catch (Exception)
                {

                    transaction.Rollback();
                    throw;
                }
            }

        }

    }
}
