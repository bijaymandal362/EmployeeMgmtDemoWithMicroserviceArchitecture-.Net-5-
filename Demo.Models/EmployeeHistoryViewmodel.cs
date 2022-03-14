using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models.ViewModel
{
    public class EmployeeHistoryViewmodel
    {
        public int EmployeeJobHistoryId { get; set; }
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Position Name is Required")]
        public int PositionId { get; set; }


        [Display(Name ="Position Name")]
       
        public string PositionName { get; set; }


        [Display(Name = "Start  Date")]
        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Start Date is Required")]
        [Remote(action: "StartDate", controller: "Employee")]
        public DateTime StartDate { get; set; }


        [Display(Name = "End  Date")]
        [Column(TypeName = "date")]
        [Required(ErrorMessage = "End Date is Required")]
        [Remote(action: "VerifyDate", controller: "Employee", ErrorMessage = "End Date is not valid to StartDate")]
        public DateTime EndDate { get; set; }
    }
}
