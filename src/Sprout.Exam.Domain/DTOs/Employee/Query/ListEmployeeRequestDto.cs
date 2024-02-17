using MediatR;

namespace Sprout.Exam.Domain.DTOs.Employee.Query
{
    public class ListEmployeeRequestDto : IRequest<ListResponseDto<EmployeeDto>>
    {
    }
}
