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
    [Index(nameof(PositionName), IsUnique= true)]
   public class Position
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PositionId { get; set; }


        [Required]
        [Column(TypeName ="NVARCHAR(100)")]
        public string PositionName { get; set; }

        public virtual ICollection<EmployeeHistory> EmployeeHistory { get; set; }

    }
}
