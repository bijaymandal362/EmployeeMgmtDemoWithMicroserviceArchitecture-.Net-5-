
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace Demo.Models
{
  public  class PositionViewModel
    {
        public int PositionId { get; set; }

        [Display(Name = "Position Name")]
        [Required(ErrorMessage = "Position Name is Required")]
        [Remote(action: "VerifyPosition", controller: "Employee")]
        [RegularExpression(@"^[a-zA-Z]+(\s[a-zA-Z]+)?$", ErrorMessage = "Please type the Position Name Correctly!!")]
        public string PositionName { get; set; }
    }
}
