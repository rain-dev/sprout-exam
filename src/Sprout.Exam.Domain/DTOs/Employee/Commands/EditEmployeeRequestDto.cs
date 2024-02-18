using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sprout.Exam.Domain.DTOs.Employee.Query;

namespace Sprout.Exam.Domain.DTOs.Employee.Commands
{
    public record EditEmployeeRequestDto : SaveEmployeeDto, IRequest<ResponseDto<ReadEmployeeDto>>
    {
        [FromRoute(Name = "id")]
        public int Id { get; set; }
    }
}
