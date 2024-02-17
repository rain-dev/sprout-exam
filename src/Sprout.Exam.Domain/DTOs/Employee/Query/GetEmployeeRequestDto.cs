using MediatR;

namespace Sprout.Exam.Domain.DTOs.Employee.Query
{
    public record GetEmployeeRequestDto : IRequest<ResponseDto<EmployeeDto>>
    {
        public int Id { get; set; }
    }
}
