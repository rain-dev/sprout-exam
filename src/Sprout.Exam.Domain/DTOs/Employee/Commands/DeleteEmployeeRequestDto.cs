using MediatR;
using Sprout.Exam.Domain.DTOs.Employee.Query;

namespace Sprout.Exam.Domain.DTOs.Employee.Commands
{
    public class DeleteEmployeeRequestDto : IRequest<ResponseDto<ReadEmployeeDto>>
    {
        public int Id { get; set; }
    }
}
