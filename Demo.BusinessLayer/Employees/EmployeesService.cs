using Demo.Entities.Data;
using Demo.Entities.Entities;
using Demo.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Demo.BusinessLayer.Employees
{
    public class EmployeesService : IEmployeesService
    {
        private readonly DemoDbContext _context;
        private readonly IToastNotification _toastNotification;

        public EmployeesService(DemoDbContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }
        public async Task DeleteEmployeeTsk(int employeeId)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {

                try
                {
                    var employee = await _context.Employee.Where(x => x.EmployeeId == employeeId).FirstOrDefaultAsync();
                    if (employee != null)
                    {
                        _context.Employee.Remove(employee);
                        _context.SaveChanges();


                        var person = await _context.Person.Where(x => x.PersonId == employee.PersonId).FirstAsync();

                        _context.Person.Remove(person);
                        _context.SaveChanges();


                        transaction.Commit();
                    }

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

        }

        public async Task<EmployeeViewModel> GetEmployeeByEmployeeId(int? employeeId)
        {
            var result = await (from eh in _context.EmployeeHistory
                                join e in _context.Employee on eh.EmployeeId equals e.EmployeeId
                                join p in _context.Position on eh.PositionId equals p.PositionId
                                join per in _context.Person on e.PersonId equals per.PersonId
                                where eh.EmployeeId == employeeId
                                select new EmployeeViewModel
                                {
                                    EmployeeJobHistoryId = eh.EmployeeJobHistoryId,
                                    EmployeeId = eh.EmployeeId,
                                    PositionId = eh.PositionId,
                                    StartDate = eh.StartDate,
                                    EndDate = eh.EndDate,
                                    Salary = e.Salary,
                                    EmployeeCode = e.EmployeeCode,
                                    PersonId = e.PersonId,
                                    IsDisabled = e.IsDisabled,
                                    FirstName = per.FirstName,
                                    MiddleName = per.MiddleName,
                                    LastName = per.LastName,
                                    Address = per.Address,
                                    Email = per.Email

                                }).FirstOrDefaultAsync();


            return result;
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetEmployee()
        {

            var result = await (from e in _context.Employee
                                join p in _context.Person on e.PersonId equals p.PersonId
                                join pos in _context.Position on e.PositionId equals pos.PositionId


                                select new EmployeeViewModel
                                {
                                    EmployeeId = e.EmployeeId,
                                    PersonId = p.PersonId,
                                    PositionId = pos.PositionId,
                                    Address = p.Address,
                                    FirstName = p.FirstName,
                                    MiddleName = p.MiddleName,
                                    PositionName = pos.PositionName,
                                    LastName = p.LastName,
                                    EmployeeCode = e.EmployeeCode,
                                    Email = p.Email,

                                }).ToListAsync();

            return result;
        }



        public async Task InsertEmployeeTsk(EmployeeViewModel employeeViewModel)
        {
            using (IDbContextTransaction transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    Employee emp = new Employee();
                    Person person = new Person();
                    if (employeeViewModel.PositionId == 0)
                    {
                        throw new Exception("PositionId is null");
                    }
                    else
                    {
                        if (person.PersonId == 0)
                        {
                            person.FirstName = employeeViewModel.FirstName;
                            person.MiddleName = employeeViewModel.MiddleName;
                            person.LastName = employeeViewModel.LastName;
                            person.Address = employeeViewModel.Address;
                            person.Email = employeeViewModel.Email;
                            await _context.Person.AddAsync(person);
                            _context.SaveChanges();

                        }
                        if (emp.EmployeeId == 0)
                        {
                            var empCode = _context.Employee.OrderByDescending(a => a.EmployeeId).FirstOrDefault();
                            if (empCode == null)
                            {
                                emp.EmployeeCode = "EMP1";

                            }
                            else
                            {
                                emp.EmployeeCode = "EMP" + (Convert.ToInt32(empCode.EmployeeCode.Substring(3, empCode.EmployeeCode.Length - 3)) + 1).ToString("D1");
                            }
                            emp.Salary = employeeViewModel.Salary;
                            emp.PositionId = Convert.ToInt32(employeeViewModel.PositionId);
                            emp.IsDisabled = employeeViewModel.IsDisabled;
                            emp.PersonId = person.PersonId;
                            await _context.Employee.AddAsync(emp);
                            _context.SaveChanges();

                        }


                        Demo.Entities.Entities.EmployeeHistory eh = new Entities.Entities.EmployeeHistory();


                        eh.EmployeeId = emp.EmployeeId;
                        eh.PositionId = Convert.ToInt32(employeeViewModel.PositionId);
                        eh.StartDate = employeeViewModel.StartDate;
                        eh.EndDate = employeeViewModel.EndDate;
                        await _context.EmployeeHistory.AddAsync(eh);
                        _context.SaveChanges();
                        transaction.Commit();

                    }

                }
                catch (Exception)
                {

                    transaction.Rollback();
                    throw;

                }
            }



        }

        public async Task UpdateEmployeeTsk(EmployeeViewModel employeeViewModel)
        {

            try
            {
                //    var result = _context.EmployeeHistory.Include("Employee").Include("Employee.Person").Where(a => a.EmployeeJobHistoryId == employeeViewModel.EmployeeJobHistoryId).FirstOrDefault();
                //    if (result != null)
                //    {
                //        result.Employee.Person.FirstName = employeeViewModel.FirstName;
                //        result.Employee.Person.MiddleName = employeeViewModel.MiddleName;
                //        result.Employee.Person.LastName = employeeViewModel.LastName;
                //        result.Employee.Person.Address = employeeViewModel.Address;
                //        result.Employee.Person.Email = employeeViewModel.Email;
                //        result.Employee.EmployeeCode = employeeViewModel.EmployeeCode;
                //        result.Employee.Salary = employeeViewModel.Salary;
                //        result.StartDate = employeeViewModel.StartDate;
                //        result.EndDate = employeeViewModel.EndDate;


                //        _context.SaveChanges();

                //    }

                var updateEmployeeTsk = await (from eh in _context.EmployeeHistory
                                               join e in _context.Employee on eh.EmployeeId equals e.EmployeeId
                                               join p in _context.Person on e.PersonId equals p.PersonId
                                               where eh.EmployeeJobHistoryId == employeeViewModel.EmployeeJobHistoryId
                                               select new EmployeeViewModel
                                               {
                                                   EmployeeJobHistoryId = eh.EmployeeJobHistoryId,
                                                   EmployeeId = eh.EmployeeId,
                                                   FirstName = p.FirstName,
                                                   MiddleName = p.MiddleName,
                                                   LastName = p.LastName,
                                                   Email = p.Email,
                                                   PersonId = p.PersonId,
                                                   EmployeeCode = e.EmployeeCode,
                                                   Salary = e.Salary,
                                                   Address = p.Address,
                                                   PositionId = eh.PositionId,
                                                   StartDate = eh.StartDate,
                                                   EndDate = eh.EndDate
                                              }).FirstAsync(); 
                if (updateEmployeeTsk != null)
                {
                    updateEmployeeTsk.EmployeeJobHistoryId = employeeViewModel.EmployeeJobHistoryId;
                    updateEmployeeTsk.EmployeeId = employeeViewModel.EmployeeId;
                    updateEmployeeTsk.PositionId = employeeViewModel.PositionId;
                    updateEmployeeTsk.FirstName = employeeViewModel.FirstName;
                    updateEmployeeTsk.MiddleName = employeeViewModel.MiddleName;
                    updateEmployeeTsk.LastName = employeeViewModel.LastName;
                    updateEmployeeTsk.Address = employeeViewModel.Address;
                    updateEmployeeTsk.Email = employeeViewModel.Email;
                    updateEmployeeTsk.EmployeeCode = employeeViewModel.EmployeeCode;
                    updateEmployeeTsk.Salary = employeeViewModel.Salary;
                    updateEmployeeTsk.StartDate = employeeViewModel.StartDate;
                    updateEmployeeTsk.EndDate = employeeViewModel.EndDate;
                   
                    _context.Entry(updateEmployeeTsk).State = EntityState.Modified;
                    _context.SaveChanges();
                }
                
            }
            catch (Exception)
            {

                throw;
            }

        }


    }

}
