using MediatR;

namespace Sprout.Exam.Domain.DTOs.Employee.Commands
{
    public record CreateEmployeeRequestDto : BaseSaveEmployeeDto, IRequest<ResponseDto<EmployeeDto>>
    {
    }
}
