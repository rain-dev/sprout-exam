using MediatR;

namespace Sprout.Exam.Domain.DTOs.Employee.Commands
{
    public class DeleteEmployeeRequestDto : IRequest<ResponseDto<EmployeeDto>>
    {
        public int Id { get; set; }
    }
}
