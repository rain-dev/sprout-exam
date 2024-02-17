using Sprout.Exam.Common.Enums;
using System;

namespace Sprout.Exam.Domain.DTOs
{

    public abstract record BaseSaveEmployeeDto
    {
        public string FullName { get; set; }
        public string Tin { get; set; }
        public DateTime Birthdate { get; set; }
        public EmployeeType TypeId { get; set; }
    }

}