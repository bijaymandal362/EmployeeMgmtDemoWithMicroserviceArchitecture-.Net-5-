using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


 namespace Demo.Entities.Entities
{
    public class EmployeeHistory
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int EmployeeJobHistoryId { get; set; }
        
        public int EmployeeId { get; set; }

        public int PositionId { get; set; }


        public DateTime StartDate { get; set; }


        public DateTime EndDate { get; set; }
       
        [ForeignKey(nameof(PositionId))]
        public virtual Position Position { get; set; }


        [ForeignKey(nameof(EmployeeId))]
        public virtual Employee Employee { get; set; }
    }
}
