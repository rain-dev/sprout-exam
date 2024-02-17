using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sprout.Exam.Domain.Models
{
    [Table("Employee")]
    public record Employee : BaseEntity<int>
    {
        public string FullName { get; set; }

        [Column("TIN")]
        public string Tin { get; set; }
        public DateTime Birthdate { get; set; }
        public int EmployeeTypeId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

}