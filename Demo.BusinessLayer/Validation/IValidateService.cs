using Demo.Models;
using Demo.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLayer.Validation
{
    public  interface IValidateService
    {
        Demo.Entities.Entities.Person VerifyEmails (string email, int PersonId);
        Demo.Entities.Entities.Position VerifyPosition(string positionName);
    }
}
