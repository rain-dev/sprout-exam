namespace Sprout.Exam.Domain.DTOs.Employee.Query
{
    public record class ReadEmployeeDto : EmployeeDto
    {
        public int Id { get; set; } 
    }
}
