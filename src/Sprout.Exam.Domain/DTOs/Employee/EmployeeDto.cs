using System.Text.Json.Serialization;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Domain.Converters;
using System;

namespace Sprout.Exam.Domain.DTOs.Employee
{
    public record EmployeeDto
    {
        public string FullName { get; set; }
        
        [JsonConverter(typeof(BirthDateConverter))]
        public DateTime Birthdate { get; set; }
        public string Tin { get; set; }
        public EmployeeType TypeId { get; set; }
        public decimal Salary { get; set; }
    }
}
