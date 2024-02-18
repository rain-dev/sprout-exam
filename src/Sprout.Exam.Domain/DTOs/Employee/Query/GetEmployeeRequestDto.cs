using MediatR;

namespace Sprout.Exam.Domain.DTOs.Employee.Query
{
    public record GetEmployeeRequestDto(int Id) : IRequest<ResponseDto<EmployeeDto>>
    {
    }
}
