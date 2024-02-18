using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprout.Exam.Domain.Models
{
    /// <summary>
    /// Employee domain model that represent database structure
    /// </summary>
    [Table("EmployeeType")]
    public record EmployeeType : BaseEntity<int>
    {
        public string TypeName { get;set; }

        [Column(TypeName = "decimal(18, 1)")]
        public decimal? Tax { get;set; }
    }

}