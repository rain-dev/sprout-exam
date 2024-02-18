using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprout.Exam.Domain.Models
{
    /// <summary>
    /// Employee domain model that represent database structure
    /// </summary>
    [Table("Employee")]
    public record Employee : BaseEntity<int>
    {
        public string FullName { get; set; }

        [Column("TIN")]
        public string Tin { get; set; }
        public DateTime Birthdate { get; set; }
        public int EmployeeTypeId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public Decimal Salary { get; set; }
        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(EmployeeTypeId))]
        public virtual EmployeeType EmployeeType { get; set; }
    }

}