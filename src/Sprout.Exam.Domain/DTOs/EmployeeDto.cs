using Sprout.Exam.Common.Enums;

namespace Sprout.Exam.Domain.DTOs
{
    public record EmployeeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Birthdate { get; set; }
        public string Tin { get; set; }
        public EmployeeType TypeId { get; set; }
    }
}
