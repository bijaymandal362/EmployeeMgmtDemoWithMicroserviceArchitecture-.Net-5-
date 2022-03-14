using Demo.BusinessLayer.Validation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;


namespace Demo.API.Controllers
{
    [EnableCors("AllowAll")]
    [Route("[controller]")]
    [ApiController]
    public class ValidationController : Controller
    {
        private readonly IValidateService _iValidateService;

        public ValidationController(IValidateService iValidateService)
        {
            _iValidateService = iValidateService;
        }

        [HttpGet]
        [Route("api/verifyEmail/{email}/{PersonId}")]
        public bool VerifyEmail(string email, int PersonId)
        {
            try
            {
                var list = _iValidateService.VerifyEmails(email, PersonId);
                if (list == null)
                {
                    return true;
                }
                else
                {
                    if (list.Email == email && list.PersonId == 0 || list.Email == email && list.PersonId != list.PersonId)
                    {
                        return false;

                    }

                }
                return false;

            }
            catch (Exception)
            {
                throw;
            }


        }

        [HttpGet]
        [Route("api/verifyPosition/{positionName}")]
        public bool VerifyPosition(string positionName)
        {
            try
            {
                var list = _iValidateService.VerifyPosition(positionName);
                if (list == null)
                {
                    return true;
                }

                return false;


            }
            catch (Exception)
            {
                return true;
            }
        }



    }
}
