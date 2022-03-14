using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models.ViewModel
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            EmployeeHistoryViewmodels = new List<EmployeeHistoryViewmodel>();
        }
        public int EmployeeJobHistoryId { get; set; }
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Position Name is Required")]
        public int PersonId { get; set; }

       
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First_Name is Required")]
        [RegularExpression(@"^[a-zA-Z]+(\s[a-zA-Z]+)?$", ErrorMessage = "Please type the First Name Correctly!!")]
        public String FirstName { get; set; }

       
        [Display(Name = "Position Name")]
       
        public string PositionName { get; set; }

      
        
        [Display(Name = "Middle Name")]
        [RegularExpression(@"^[a-zA-Z]+(\s[a-zA-Z]+)?$", ErrorMessage = "Please type the Middle Name Correctly!!")]
        public String MiddleName { get; set; }

       
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last_Name is Required")]
        [RegularExpression(@"^[a-zA-Z]+(\s[a-zA-Z]+)?$", ErrorMessage = "Please type the Last Name Correctly!!")]
        public String LastName { get; set; }

      
        
        [Display(Name ="Full Name")]
        public string FullName { get { return string.Concat(FirstName + " " +MiddleName+" "+ LastName); } }
        public int PositionId { get; set; }

       

        [Required(ErrorMessage = "Salary is Required")]
        [RegularExpression(@"^(?!-|0(?:\.0*)?$)\d+(?:\.\d+)?$", ErrorMessage ="Salary will always positve integer number value!!")]
        public decimal Salary { get; set; }



        [Required(ErrorMessage = "Start Date is Required")]

        [Remote(action: "StartDate", controller: "Employee")]
        public DateTime StartDate { get; set; }



        [Required(ErrorMessage = "End Date is Required")]
     
        [Remote(action: "VerifyDate", controller: "Employee", ErrorMessage = "End Date is not valid to StartDate")]
        public DateTime EndDate { get; set; }




        [Display(Name = "Employee Code")]
      
        public string EmployeeCode { get; set; }



        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }



        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please type Correct Formate of Email")]

        //[Remote(action: "VerifyEmail", controller: "Employee", AdditionalFields = "PersonId", ErrorMessage = "Email already in use")]
      
        public string Email { get; set; }
        
        
        public bool IsDisabled { get; set; }

       
        public List<EmployeeHistoryViewmodel> EmployeeHistoryViewmodels { get; set; }


    }
}
