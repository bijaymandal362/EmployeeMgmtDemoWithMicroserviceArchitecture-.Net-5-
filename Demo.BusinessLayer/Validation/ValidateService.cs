using Demo.Entities.Data;
using Demo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLayer.Validation
{
    public class ValidateService : IValidateService
    {
        private readonly DemoDbContext _iDemoDbContext;

        public ValidateService(DemoDbContext iDemoDbContext)
        {
            _iDemoDbContext = iDemoDbContext;
        }

        

        public Entities.Entities.Person VerifyEmails(string email,int PersonId)
        {
            using (IDbContextTransaction transaction = _iDemoDbContext.Database.BeginTransaction())
            {
                try
                {
                   
                    List<Entities.Entities.Person> person = new List<Entities.Entities.Person>();
                    var post = _iDemoDbContext.Person.Where(x => x.Email == email && x.PersonId != PersonId).FirstOrDefault();
                    if (post != null)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        throw new Exception();
                    }
                    return post;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                   
                }
            }

        }

        public Entities.Entities.Position VerifyPosition(string position)
        {
            return _iDemoDbContext.Position.Where(x => x.PositionName == position).FirstOrDefault();
        }
    }
}
