namespace Sprout.Exam.Domain.DTOs
{
    public record EditEmployeeDto: BaseSaveEmployeeDto
    {
        public int Id { get; set; }
    }
}
