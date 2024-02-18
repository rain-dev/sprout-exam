using MediatR;
using Sprout.Exam.Domain.DTOs.Employee.Query;

namespace Sprout.Exam.Domain.DTOs.Employee.Commands
{
    /// <summary>
    /// A command DTO for creating a employee record
    /// </summary>
    public record CreateEmployeeRequestDto : SaveEmployeeDto, IRequest<ResponseDto<ReadEmployeeDto>>
    {
    }
}
