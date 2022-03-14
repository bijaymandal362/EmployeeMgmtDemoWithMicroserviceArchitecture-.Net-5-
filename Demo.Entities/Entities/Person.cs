
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Demo.Entities.Entities
{
    [Index(nameof(Email), IsUnique =true)]
    public class Person
    {
        
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(50)]
        public string MiddleName { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(50)]
        public string LastName { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        public string Address { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(100)]
        [Remote(action: "VerifyEmail", controller: "Emp", ErrorMessage = "Email already in use")]
        public string Email { get; set; }

        public virtual ICollection<Employee> Employee { get; set; }

    }
}
