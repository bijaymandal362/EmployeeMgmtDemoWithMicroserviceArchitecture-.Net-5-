using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Entities.Entities
{

    [Index(nameof(EmployeeCode), IsUnique = true)]

    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int EmployeeId { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Salary { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(20)")]
        public string EmployeeCode { get; set; }

        public int? PositionId { get; set; }

        [ForeignKey(nameof(PositionId))]
        public virtual Position Position { get; set; }

        public bool IsDisabled { get; set; }

        [ForeignKey(nameof(PersonId))]
        public virtual Person Person { get; set; }

        public virtual ICollection<EmployeeHistory> EmployeeHistory { get; set; }
    }
}
